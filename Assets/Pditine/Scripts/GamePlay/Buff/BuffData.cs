using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    /// <summary>
    /// Buff增加时的更新方式
    /// </summary>
    public enum BuffAttachType
    {
        Add,        // 增加层数，时间叠加
        Override,   // 覆写
        Keep        // 无法叠加
    }

    /// <summary>
    /// Buff时间到时的更新方式
    /// </summary>
    public enum BuffLostType
    {
        Reduce,     // 减少一层
        Clear       // 全部移除
    }

    [CreateAssetMenu(fileName = "BuffData", menuName = "AssAssIn/BuffData")]
    public class BuffData : ScriptableObject
    {
        [Title("Base Info")]
        public int id;
        public string buffName;
        public string description;
        public Sprite icon;
        public int priority;
        public int maxStack;
        public string[] tags;

        [Title("Time Info")]
        public bool willLastForever;
        public float durationTime;
        public float tickTime;

        [Title("Update Type")]
        public BuffAttachType attachType;
        public BuffLostType lostType;

        [Title("Callback Event")]
        [InlineEditor] public BuffEvent[] onAttachEvents;
        [InlineEditor] public BuffEvent[] onLostEvents;
        [InlineEditor] public BuffEvent[] onTickEvents;
        [InlineEditor] public BuffEvent[] onResetEvents;
    }
}