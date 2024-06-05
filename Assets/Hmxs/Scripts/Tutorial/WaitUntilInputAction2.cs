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
                if (Tutorial1GameManager.Instance.Player1Controller.InputHandler.IsKeyboard)
                {
                    if (inputAction.action.WasPerformedThisFrame())
                        _operationTime += Time.deltaTime;
                }
                else if (inputAction.action.inProgress)
                        _operationTime += Time.deltaTime;
                yield return null;
            }
            Continue();
        }
    }
}