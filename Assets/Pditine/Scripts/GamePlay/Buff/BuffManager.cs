using System;
using System.Collections.Generic;
using System.Linq;
using Pditine.Player;
using PurpleFlowerCore;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Pditine.GamePlay.Buff
{
    public class BuffManager : MonoBehaviour
    {
#if UNITY_EDITOR
    [SerializeField] [ReadOnly] private List<BuffInfo> buffList = new();
#endif
    private readonly SortedSet<BuffInfo> _buffSet = new();
    public SortedSet<BuffInfo> BuffSet => _buffSet;
    
    private readonly SortedSet<BuffInfo> _buffBufferSet = new();

    private readonly SortedSet<BuffInfo> _initializedBuffSet = new();
    
    public event UnityAction<BuffInfo> OnAttachBuff;
    public event UnityAction<BuffInfo> OnLostBuff;

    public event UnityAction OnReset;

    public static BuffManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("单例重复");
            Destroy(gameObject);
        }
    }
    
    public void Init(PlayerController player1,PlayerController player2)
    {
        OnReset += player1.ResetProperty;
        OnReset += player2.ResetProperty;
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        buffList = _buffSet.ToList();
#endif
        UpdateBuffTimer();
    }

    private void UpdateBuffTimer()
    {
        foreach (var buffInfo in _buffSet)
        {
            // Update Tick Timer
            if (buffInfo.buffData.onTickEvents != null)
            {
                if (buffInfo.tickCounter < 0)
                {
                    buffInfo.tickCounter = buffInfo.buffData.tickTime;
                    buffInfo.OnTick();
                }
                else
                    buffInfo.tickCounter -= Time.deltaTime;
            }

            // Update Duration Timer
            if (buffInfo.durationCounter < 0)
                _buffBufferSet.Add(buffInfo);
            else
            {
                if (buffInfo.durationCounter<10000)
                    buffInfo.durationCounter -= Time.deltaTime;
            }
        }

        foreach (var buffInfo in _buffBufferSet)
            LostBuff(buffInfo);
        _buffBufferSet.Clear();
    }

    private BuffInfo GetBuff(BuffInfo other)
    {
        foreach (var buff in _buffSet)
        {
            if (buff.buffData.id == other.buffData.id && buff.target.ID == other.target.ID)
                return buff;
        }
        
        return null;
    }

    #region Public Methods

    public void AttachBuff(BuffInfo buffInfo)
    {
        if (!_initializedBuffSet.Contains(buffInfo))
        {
            buffInfo.OnInit();
            _initializedBuffSet.Add(buffInfo);
        }
        
        //if (_buffSet.Contains(buffInfo))
        if (_buffSet.Contains(buffInfo)&& buffInfo.buffData.attachType != BuffAttachType.Keep)
        {
            // buff存在
            var buff = GetBuff(buffInfo);
            if (buff.currentStack < buff.buffData.maxStack)
            {
                // 当前buff层数小于最大层数
                buff.currentStack++;
                buff.durationCounter = buff.buffData.attachType switch
                {
                    BuffAttachType.Add => buff.durationCounter + buff.buffData.durationTime,
                    BuffAttachType.Override => buff.buffData.durationTime,
                    _ => buff.durationCounter
                };
                
                buff.OnAttach();
                OnAttachBuff?.Invoke(buffInfo);
                ResetBuff();
            }
            else
            {
                // buff已到最大层数
                buff.durationCounter = buff.buffData.attachType switch
                {
                    BuffAttachType.Add => buff.buffData.durationTime * buff.buffData.maxStack,
                    BuffAttachType.Override => buff.buffData.durationTime,
                    _ => buff.durationCounter
                };
            }

            return;
        }
        // buff不存在
        buffInfo.durationCounter = buffInfo.buffData.durationTime;
        buffInfo.tickCounter = buffInfo.buffData.tickTime;
        buffInfo.OnAttach();
        OnAttachBuff?.Invoke(buffInfo);
        _buffSet.Add(buffInfo);
    }

    public void LostBuff(BuffInfo buffInfo)
    {
        if (!_buffSet.Contains(buffInfo))
            return;
        var buff = GetBuff(buffInfo);
        switch (buff.buffData.lostType)
        {
            case BuffLostType.Reduce:
                buff.currentStack--;
                if (buff.currentStack < 0)
                {
                    _buffSet.Remove(buff);
                    buff.OnLost();
                    OnLostBuff?.Invoke(buff);
                }
                else
                    buff.durationCounter = buff.buffData.durationTime;
                break;
            case BuffLostType.Clear:
                buff.OnLost();
                OnLostBuff?.Invoke(buff);
                _buffSet.Remove(buff);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        ResetBuff();
    }

    #endregion

    private void ResetBuff()
    {
        OnReset?.Invoke();
        foreach (var buff in _buffSet)
        {
            for (int i = -1; i < buff.currentStack; i++)
            {
                buff.OnReset();
            }
        }
    }
    }
}