using System.Collections;
using Fungus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs.Scripts.Tutorial
{
    [CommandInfo("Input",
        "WaitUntilInputAction2",
        "Continue until the input action is triggered")]
    [AddComponentMenu("")]
    public class WaitUntilInputAction2 : Command
    {
        [SerializeField] protected InputActionReference inputAction;
        [SerializeField] protected float operationTime;
        [SerializeField] protected float securedOperationTime;
        private float _operationTime;
        private float _securedOperationTime;
        public override void OnEnter()
        {
            _operationTime = 0;
            _securedOperationTime = 0;
            StartCoroutine(CheckInput());
        }

        private IEnumerator CheckInput()
        {
            while (_operationTime < operationTime || _securedOperationTime < securedOperationTime)
            {
                if (inputAction.action.inProgress)
                        _operationTime += Time.unscaledDeltaTime;
                _securedOperationTime += Time.unscaledDeltaTime;
                yield return null;
            }
            Continue();
        }
    }
}