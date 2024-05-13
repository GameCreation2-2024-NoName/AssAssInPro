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

    private BuffInfo GetBuffById(int id) => _buffSet.First(buffInfo => buffInfo.buffData.id == id);

    #region Public Methods

    public void AttachBuff(BuffInfo buffInfo)
    {
        //if (_buffSet.Contains(buffInfo))
        if (_buffSet.Contains(buffInfo)&& buffInfo.buffData.attachType != BuffAttachType.Keep)
        {
            // buff存在
            var buff = GetBuffById(buffInfo.buffData.id);
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
        {
            PFCLog.Warning("无对应buff");
            return;
        }
        switch (buffInfo.buffData.lostType)
        {
            case BuffLostType.Reduce:
                buffInfo.currentStack--;
                buffInfo.OnLost();
                if (buffInfo.currentStack <= 0)
                    _buffSet.Remove(buffInfo);
                else
                    buffInfo.durationCounter = buffInfo.buffData.durationTime;
                break;
            case BuffLostType.Clear:
                buffInfo.OnLost();
                _buffSet.Remove(buffInfo);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        OnLostBuff?.Invoke(buffInfo);
        ResetBuff();
    }

    #endregion

    private void ResetBuff()
    {
        OnReset?.Invoke();
        foreach (var buff in _buffSet)
        {
            buff.OnReset();
        }
    }
    }
}