using UnityEngine;

namespace Fungus
{
    [CommandInfo("Scripting",
        "RemoveComponent",
        "Removes a component from a game object.")]
    [AddComponentMenu("")]
    public class RemoveComponent : Command
    {
        [Tooltip("The game object to remove the component from")]
        [SerializeField] protected GameObject targetGameObject;

        [Tooltip("The type of component to remove")]
        [SerializeField] protected Component component;

        public override void OnEnter()
        {
            if (targetGameObject != null && component != null)
            {
                Destroy(component);
            }

            Continue();
        }
    }
}