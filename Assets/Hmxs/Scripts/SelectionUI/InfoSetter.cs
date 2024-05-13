using Pditine.Data.Ass;
using Pditine.Scripts.Data.Ass;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class InfoSetter : MonoBehaviour
    {
        [SerializeField] private int id;
        [Title("Ass Info")]
        [SerializeField] private EquipUI assImg;
        [SerializeField] private TMP_Text assName;
        [SerializeField] private InfoUI assProperty1;
        [SerializeField] private InfoUI assProperty2;
        [SerializeField] private InfoUI assProperty3;
        [SerializeField] private TMP_Text assIntroduction;
        [SerializeField] [MinMaxSlider(0, 20, true)] private Vector2 assProperty1Range;
        [SerializeField] [MinMaxSlider(0, 40, true)] private Vector2 assProperty2Range;
        [SerializeField] [MinMaxSlider(0, 20, true)] private Vector2 assProperty3Range;

        [Title("Thorn Info")]
        [SerializeField] private EquipUI thornImg;
        [SerializeField] private TMP_Text thornName;
        [SerializeField] private InfoUI thornProperty1;
        [SerializeField] private InfoUI thornProperty2;
        [SerializeField] private InfoUI thornProperty3;
        [SerializeField] private TMP_Text thornIntroduction;
        [SerializeField] [MinMaxSlider(0, 20, true)] private Vector2 thornProperty1Range;
        [SerializeField] [MinMaxSlider(0, 20, true)] private Vector2 thornProperty2Range;
        [SerializeField] [MinMaxSlider(0, 20, true)] private Vector2 thornProperty3Range;

        private void SetAssImg(Sprite sprite)
        {
            assImg.Sprite = sprite;
            assImg.SetNativeSize();
        }
        private void SetAssName(string nameOfAss) => assName.text = nameOfAss;
        private void SetAssProperty1(float value) => assProperty1.SetValue(value);
        private void SetAssProperty2(float value) => assProperty2.SetValue(value);
        private void SetAssProperty3(float value) => assProperty3.SetValue(value);
        private void SetAssIntroduction(string introduction) => assIntroduction.text = introduction;

        private void SetThornImg(Sprite sprite)
        {
            thornImg.Sprite = sprite;
            thornImg.SetNativeSize();
        }
        private void SetThornName(string nameOfThorn) => thornName.text = nameOfThorn;
        private void SetThornProperty1(float value) => thornProperty1.SetValue(value);
        private void SetThornProperty2(float value) => thornProperty2.SetValue(value);
        private void SetThornProperty3(float value) => thornProperty3.SetValue(value);
        private void SetThornIntroduction(string introduction) => thornIntroduction.text = introduction;

        private static float DataMapping(float value, Vector2 range)
        {
            value = Mathf.Clamp(value, range.x, range.y);
            return (value - range.x) / (range.y - range.x);
        }

        public void SetAssInfo(AssDataBase assInfo)
        {
            if (assInfo == null)
            {
                Debug.LogError($"Cannot Get AssInfo");
                return;
            }

            var assSprite = id == 1 ? assInfo.PortraitBlue : assInfo.PortraitYellow;
            SetAssImg(assSprite);
            SetAssName(assInfo.AssName);

            SetAssProperty1(DataMapping(assInfo.HP, assProperty1Range));
            SetAssProperty2(DataMapping(assInfo.InitialVelocity, assProperty2Range));
            SetAssProperty3(DataMapping(assInfo.Weight, assProperty3Range));

            SetAssIntroduction(assInfo.AssIntroduction);
        }

        public void SetThornInfo(ThornDataBase thornInfo)
        {
            if (thornInfo == null)
            {
                Debug.LogError($"Cannot Get ThornInfo");
                return;
            }

            SetThornImg(thornInfo.Portrait);
            SetThornName(thornInfo.ThornName);

            SetThornProperty1(DataMapping(thornInfo.ATK, thornProperty1Range));
            SetThornProperty2(DataMapping(thornInfo.CD, thornProperty2Range));
            SetThornProperty3(DataMapping(thornInfo.Weight, thornProperty3Range));

            SetThornIntroduction(thornInfo.ThornIntroduction);
        }
    }
}