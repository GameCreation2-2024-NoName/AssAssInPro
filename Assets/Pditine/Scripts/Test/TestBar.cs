using PurpleFlowerCore.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Utility
{
    public class TestBar : MonoBehaviour
    {
        [SerializeField]private PropertyBar propertyBar;
        private float value = 10;
        private int current;
        [SerializeField] private Image edge;
        [SerializeField] private float offset;

        public void Update()
        {
            if( Input.GetKeyDown(KeyCode.Space))
            {
                current++;
            }
        
            propertyBar.Value = Mathf.Lerp(propertyBar.Value, current / value, Time.deltaTime);
            edge.transform.position = propertyBar.EdgePosition + new Vector3(offset,0,0);
        }
    }
}