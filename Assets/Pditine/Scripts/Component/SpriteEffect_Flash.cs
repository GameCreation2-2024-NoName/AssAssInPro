using System.Collections.Generic;
using UnityEngine;

namespace Pditine.Component
{
    public class SpriteEffect_Flash : MonoBehaviour
    {
        // [SerializeField] private List<SpriteRenderer> spriteRendererGroup = new();
        // [SerializeField] private List<Material> materialBuffer = new();
        [SerializeField] private Material flashMaterial;
        private SpriteRenderer TheSpriteRenderer => GetComponent<SpriteRenderer>();
        private Material _materialBuffer;
        public void FlashOn()
        {
            // foreach (var spriteRenderer in spriteRendererGroup)
            // {
            //     materialBuffer.Add(spriteRenderer.material);
            //     spriteRenderer.material = flashMaterial;
            // }
            if (TheSpriteRenderer is null) return;
            if (TheSpriteRenderer.material != flashMaterial) _materialBuffer = TheSpriteRenderer.material;
            TheSpriteRenderer.material = flashMaterial;
        }

        public void FlashOff()
        {
            // for (int i = 0; i < spriteRendererGroup.Count; i++)
            // {
            //     spriteRendererGroup[i].material = materialBuffer[i];
            // }
            // materialBuffer.Clear();
            if (TheSpriteRenderer is null) return;
            TheSpriteRenderer.material = _materialBuffer;
        }
    }
}