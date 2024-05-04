using System;
using Hmxs.Toolkit.Base.Bindable;
using Pditine.Audio;
using Pditine.Scripts.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class PlayerSelection : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private InfoSetter infoSetter;
        [SerializeField] private EquipUI assOutline;
        [SerializeField] private EquipUI thornOutline;
        [SerializeField] private ReadyUI readyUI;

        [SerializeField] [ReadOnly] private bool isReady;
        [SerializeField] [ReadOnly] private bool canSelect = true;

        [SerializeField] [ReadOnly] private BindableProperty<int> assId = new() { Value = 0 };
        [SerializeField] [ReadOnly] private BindableProperty<int> thornId = new() { Value = 0 };
        [SerializeField] [ReadOnly] private bool isAssSelected = true;

        public bool IsReady => isReady;
        public Action onConfirm;

        private InputHandler InputHandler =>
            id == 1 ? PlayerManager.Instance.Handler1 : PlayerManager.Instance.Handler2;

        private void Start()
        {
            assId.onValueChanged += value =>
            {
                infoSetter.SetAssInfo(DataManager.Instance.GetAssData(value));
            };
            thornId.onValueChanged += value =>
            {
                infoSetter.SetThornInfo(DataManager.Instance.GetThornData(value));
            };

            infoSetter.SetAssInfo(DataManager.Instance.GetAssData(assId.Value));
            infoSetter.SetThornInfo(DataManager.Instance.GetThornData(thornId.Value));
            //SetOutline();
        }

        private void OnDestroy()
        {
            assId.onValueChanged -= value =>
            {
                infoSetter.SetAssInfo(DataManager.Instance.GetAssData(value));
            };
            thornId.onValueChanged -= value =>
            {
                infoSetter.SetThornInfo(DataManager.Instance.GetThornData(value));
            };
        }

        private void Update()
        {
            if (InputHandler == null) return;

            if (InputHandler.Confirm)
                OnConfirm();

            if (InputHandler.Select != Vector2.zero && !isReady && canSelect)
                OnSelect(InputHandler.Select);
            else if (InputHandler.Select == Vector2.zero)
                canSelect = true;
        }

        private void OnSelect(Vector2 select)
        {
            canSelect = false;
            var angle = Mathf.Atan2(select.y, select.x) * Mathf.Rad2Deg;
            switch (angle)
            {
                case >= 45 and <= 135:      // Up
                    Next();
                    break;
                case >= -135 and <= -45:    // Down
                    Last();
                    break;
                case >= -45 and <= 45:      // Right
                    SetSelection(!isAssSelected);
                    break;
                default:                    // Left
                    SetSelection(!isAssSelected);
                    break;
            }
        }

        public void OnConfirm()
        {
            if (InputHandler is null) return;
            AAIAudioManager.Instance.PlayEffect("按下按钮");
            Debug.Log("Confirm");
            isReady = !isReady;
            
            readyUI.SetReady(isReady);
            
            if (isReady)
            {
                CloseOutline();
                switch (id)
                {
                    case 1:
                        DataManager.Instance.PassingData.player1AssID = assId.Value;
                        DataManager.Instance.PassingData.player1ThornID = thornId.Value;
                        break;
                    case 2:
                        DataManager.Instance.PassingData.player2AssID = assId.Value;
                        DataManager.Instance.PassingData.player2ThornID = thornId.Value;
                        break;
                }

                onConfirm?.Invoke();
            }
            else
            {
                SetOutline();
            }
        }

        public void Next()
        {
            if (isAssSelected)
                NextAss();
            else
                NextThorn();
        }

        public void Last()
        {
            if (isAssSelected)
                PreviousAss();
            else
                PreviousThorn();
        }

        private void NextAss() =>
            assId.Value = assId.Value >= DataManager.Instance.Asses.Count - 1 ? 0 : assId.Value + 1;

        private void NextThorn() =>
            thornId.Value = thornId.Value >= DataManager.Instance.Thorns.Count - 1 ? 0 : thornId.Value + 1;

        private void PreviousAss() =>
            assId.Value = assId.Value <= 0 ? DataManager.Instance.Asses.Count - 1 : assId.Value - 1;

        private void PreviousThorn() =>
            thornId.Value = thornId.Value <= 0 ? DataManager.Instance.Thorns.Count - 1 : thornId.Value - 1;

        private void SetOutline()
        {
            if (isAssSelected)
            {
                assOutline.IsSelected(true);
                thornOutline.IsSelected(false);
            }
            else
            {
                assOutline.IsSelected(false);
                thornOutline.IsSelected(true);
            }
        }

        public void CloseOutline()
        {
            assOutline.IsSelected(false);
            thornOutline.IsSelected(false);
        }

        public void SetSelection(bool isAss)
        {
            isAssSelected = isAss;
            SetOutline();
        }
    }
}