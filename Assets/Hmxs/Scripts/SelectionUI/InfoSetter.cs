using Pditine.Scripts.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class InfoSetter : MonoBehaviour
    {
        [Title("Ass Info")]
        [SerializeField] private Image assImg;
        [SerializeField] private TMP_Text assName;
        [SerializeField] private Slider assProperty1;
        [SerializeField] private Slider assProperty2;
        [SerializeField] private Slider assProperty3;
        [SerializeField] private TMP_Text assIntroduction;

        [Title("Thorn Info")]
        [SerializeField] private Image thornImg;
        [SerializeField] private TMP_Text thornName;
        [SerializeField] private Slider thornProperty1;
        [SerializeField] private Slider thornProperty2;
        [SerializeField] private Slider thornProperty3;
        [SerializeField] private TMP_Text thornIntroduction;

        private void SetAssImg(Sprite sprite)
        {
            assImg.sprite = sprite;
            assImg.SetNativeSize();
        }

        private void SetAssName(string nameOfAss) => assName.text = nameOfAss;
        private void SetAssProperty1(float value) => assProperty1.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetAssProperty2(float value) => assProperty2.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetAssProperty3(float value) => assProperty3.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetAssIntroduction(string introduction) => assIntroduction.text = introduction;

        private void SetThornImg(Sprite sprite)
        {
            thornImg.sprite = sprite;
            thornImg.SetNativeSize();
        }
        private void SetThornName(string nameOfThorn) => thornName.text = nameOfThorn;
        private void SetThornProperty1(float value) => thornProperty1.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetThornProperty2(float value) => thornProperty2.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetThornProperty3(float value) => thornProperty3.value =  Mathf.Clamp(value, 0f, 1f);
        private void SetThornIntroduction(string introduction) => thornIntroduction.text = introduction;

        public void SetAssInfo(int assId)
        {
            var assInfo = DataManager.Instance.GetAssData(assId);
            if (assInfo == null)
            {
                Debug.LogError($"Cannot Find AssInfo At {assId}");
                return;
            }

            SetAssImg(assInfo.Portrait);
            SetAssName(assInfo.AssName);

            // todo: 需要进行数据映射
            SetAssProperty1(assInfo.HP);
            SetAssProperty2(assInfo.InitialVelocity);
            SetAssProperty3(assInfo.Friction);

            SetAssIntroduction(assInfo.AssIntroduction);
        }

        public void SetThornInfo(int thornId)
        {
            var thornInfo = DataManager.Instance.GetThornData(thornId);
            if (thornInfo == null)
            {
                Debug.LogError($"Cannot Find ThornInfo At {thornId}");
                return;
            }

            SetThornImg(thornInfo.Portrait);
            SetThornName(thornInfo.ThornName);

            // todo: 需要进行数据映射
            SetThornProperty1(thornInfo.ATK);
            SetThornProperty2(thornInfo.CD);
            SetThornProperty3(thornInfo.Friction);

            SetThornIntroduction(thornInfo.ThornIntroduction);
        }
    }
}