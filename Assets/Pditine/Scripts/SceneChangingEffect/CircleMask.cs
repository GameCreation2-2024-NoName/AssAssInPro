using System.Collections;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pditine.Utility
{
    public class CircleMask : MonoBehaviour
    {
        [SerializeField][Range(0,1)] private float speed;

        public void Cover(Vector3 pos,UnityAction callBack = null)
        {
            StartCoroutine(DoCover(pos,callBack));
            
        }
        
        private IEnumerator DoCover(Vector3 pos,UnityAction callBack)
        {
            transform.position = pos;
            while (transform.localScale.x>0.5f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0,0,1), speed);
                yield return new WaitForSeconds(0.01f);
            }
            callBack?.Invoke();
        }
    }
}