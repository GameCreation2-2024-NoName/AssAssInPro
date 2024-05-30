using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fungus
{
    [CommandInfo("Input",
        "WaitUntilInputAction2",
        "Continue until the input action is triggered")]
    [AddComponentMenu("")]
    public class WaitUntilInputAction2 : Command
    {
        [SerializeField] protected InputActionReference inputAction;
        [SerializeField] protected float operationTime;
        [SerializeField] protected float delay;
        private float _operationTime;
        public override void OnEnter()
        {
            _operationTime = 0;
            StartCoroutine(CheckInput());
        }

        private IEnumerator CheckInput()
        {
            while (_operationTime < operationTime)
            {
                if (inputAction.action.WasPerformedThisFrame())
                    _operationTime += Time.deltaTime;
                yield return null;
            }
            Invoke(nameof(InputActionIsPerformed), delay);
        }

        private void InputActionIsPerformed()
        {
            Continue();
        }
    }
}