// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using UnityEngine.EventSystems;
 using UnityEngine.InputSystem.UI;

namespace Fungus
{
    /// <summary>
    /// Supported modes for clicking through a Say Dialog.
    /// </summary>
    public enum ClickMode
    {
        /// <summary> Clicking disabled. </summary>
        Disabled,
        /// <summary> Click anywhere on screen to advance. </summary>
        ClickAnywhere,
        /// <summary> Click anywhere on Say Dialog to advance. </summary>
        ClickOnDialog,
        /// <summary> Click on continue button to advance. </summary>
        ClickOnButton
    }

    public enum InputMode
    {
        Both,
        Old,
        New
    }

    /// <summary>
    /// Input handler for say dialogs.
    /// </summary>
    public class DialogInput : MonoBehaviour
    {
        [Tooltip("Click to advance story")]
        [SerializeField] protected ClickMode clickMode;

        [SerializeField] protected InputMode inputMode = InputMode.Both;

        [Tooltip("Delay between consecutive clicks. Useful to prevent accidentally clicking through story.")]
        [SerializeField] protected float nextClickDelay = 0f;

        [Tooltip("Allow holding Cancel to fast forward text")]
        [SerializeField] protected bool cancelEnabled = true;

        [Tooltip("Ignore input if a Menu dialog is currently active")]
        [SerializeField] protected bool ignoreMenuClicks = true;

        protected bool DialogClickedFlag;

        protected bool NextLineInputFlag;

        protected float IgnoreClickTimer;

        protected StandaloneInputModule OldInputModule;
        protected InputSystemUIInputModule NewInputModule;

        protected Writer Writer;

        protected virtual void Awake()
        {
            Writer = GetComponent<Writer>();

            CheckEventSystem();
        }

        // There must be an Event System in the scene for Say and Menu input to work.
        // This method will automatically instantiate one if none exists.
        protected virtual void CheckEventSystem()
        {
            EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                // Auto spawn an Event System from the prefab
                var prefab = Resources.Load<GameObject>("Prefabs/EventSystem");
                if (prefab != null)
                {
                    GameObject go = Instantiate(prefab) as GameObject;
                    go.name = "EventSystem";
                }
            }
        }

        protected virtual void Update()
        {
            if (EventSystem.current == null)
            {
                return;
            }

            if (inputMode == InputMode.Both)
                CheckInputMode();

            switch (inputMode)
            {
                case InputMode.Old:
                    OldInputUpdate();
                    break;
                case InputMode.New:
                    NewInputUpdate();
                    break;
            }

            if (IgnoreClickTimer > 0f)
            {
                IgnoreClickTimer = Mathf.Max (IgnoreClickTimer - Time.deltaTime, 0f);
            }

            if (ignoreMenuClicks)
            {
                // Ignore input events if a Menu is being displayed
                if (MenuDialog.ActiveMenuDialog != null &&
                    MenuDialog.ActiveMenuDialog.IsActive() &&
                    MenuDialog.ActiveMenuDialog.DisplayedOptionsCount > 0)
                {
                    DialogClickedFlag = false;
                    NextLineInputFlag = false;
                }
            }

            // Tell any listeners to move to the next line
            if (NextLineInputFlag)
            {
                var inputListeners = gameObject.GetComponentsInChildren<IDialogInputListener>();
                for (int i = 0; i < inputListeners.Length; i++)
                {
                    var inputListener = inputListeners[i];
                    inputListener.OnNextLineEvent();
                }
                NextLineInputFlag = false;
            }
        }

        private void CheckInputMode()
        {
            if (NewInputModule == null)
                NewInputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
            if (NewInputModule != null)
            {
                inputMode = InputMode.New;
                return;
            }
            if (OldInputModule == null)
                OldInputModule = EventSystem.current.GetComponent<StandaloneInputModule>();
            if (OldInputModule != null)
            {
                inputMode = InputMode.Old;
                return;
            }
            Debug.LogError("Can't find Input Module on EventSystem");
        }

        private void OldInputUpdate()
        {
            if (OldInputModule == null)
            {
                OldInputModule = EventSystem.current.GetComponent<StandaloneInputModule>();
                if (OldInputModule == null) Debug.LogError("Can't Find OldInputModule");
            }

            if (Writer != null)
            {
                if (Input.GetButtonDown(OldInputModule.submitButton) ||
                    (cancelEnabled && Input.GetButton(OldInputModule.cancelButton)))
                {
                    SetNextLineFlag();
                }
            }

            switch (clickMode)
            {
            case ClickMode.Disabled:
                break;
            case ClickMode.ClickAnywhere:
                if (Input.GetMouseButtonDown(0))
                {
                    SetClickAnywhereClickedFlag();
                }
                break;
            case ClickMode.ClickOnDialog:
                if (DialogClickedFlag)
                {
                    SetNextLineFlag();
                    DialogClickedFlag = false;
                }
                break;
            }
        }

        private void NewInputUpdate()
        {
            if (NewInputModule == null)
            {
                NewInputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
                if (NewInputModule == null) Debug.LogError("Can't Find NewInputModule");
            }

            if (Writer != null)
            {
                if (NewInputModule.submit.action.WasPressedThisFrame() ||
                    (cancelEnabled && NewInputModule.cancel.action.WasPressedThisFrame()))
                {
                    SetNextLineFlag();
                }
            }

            switch (clickMode)
            {
                case ClickMode.Disabled:
                    break;
                case ClickMode.ClickAnywhere:
                    if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        SetClickAnywhereClickedFlag();
                    }
                    break;
                case ClickMode.ClickOnDialog:
                    if (DialogClickedFlag)
                    {
                        SetNextLineFlag();
                        DialogClickedFlag = false;
                    }
                    break;
            }
        }

        #region Public members

        /// <summary>
        /// Trigger next line input event from script.
        /// </summary>
        public virtual void SetNextLineFlag()
        {
            if(Writer.IsWaitingForInput || Writer.IsWriting)
            {
                NextLineInputFlag = true;
            }
        }
        /// <summary>
        /// Set the ClickAnywhere click flag.
        /// </summary>
        public virtual void SetClickAnywhereClickedFlag()
        {
            if (IgnoreClickTimer > 0f)
            {
                return;
            }
            IgnoreClickTimer = nextClickDelay;

            // Only applies if ClickedAnywhere is selected
            if (clickMode == ClickMode.ClickAnywhere)
            {
                SetNextLineFlag();
            }
        }
        /// <summary>
        /// Set the dialog clicked flag (usually from an Event Trigger component in the dialog UI).
        /// </summary>
        public virtual void SetDialogClickedFlag()
        {
            // Ignore repeat clicks for a short time to prevent accidentally clicking through the character dialogue
            if (IgnoreClickTimer > 0f)
            {
                return;
            }
            IgnoreClickTimer = nextClickDelay;

            // Only applies in Click On Dialog mode
            if (clickMode == ClickMode.ClickOnDialog)
            {
                DialogClickedFlag = true;
            }
        }

        /// <summary>
        /// Sets the button clicked flag.
        /// </summary>
        public virtual void SetButtonClickedFlag()
        {
            // Only applies if clicking is not disabled
            if (clickMode != ClickMode.Disabled)
            {
                SetNextLineFlag();
            }
        }

        #endregion
    }
}
