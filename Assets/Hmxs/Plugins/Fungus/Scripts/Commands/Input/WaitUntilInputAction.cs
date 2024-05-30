using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fungus
{
    [CommandInfo("Input",
        "WaitUntilInputAction",
        "Continue until the input action is triggered")]
    [AddComponentMenu("")]
    public class WaitUntilInputAction : Command
    {
        [SerializeField] protected InputActionReference inputAction;
        [SerializeField] protected float delay;

        public override void OnEnter()
        {
            StartCoroutine(CheckInput());
        }

        private IEnumerator CheckInput()
        {
            while (!inputAction.action.WasPerformedThisFrame())
                yield return null;
            Invoke(nameof(InputActionIsPerformed), delay);
        }

        private void InputActionIsPerformed()
        {
            Continue();
        }
    }
}