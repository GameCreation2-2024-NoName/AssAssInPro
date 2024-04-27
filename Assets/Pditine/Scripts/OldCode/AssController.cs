using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.OldCode
{
    public class AssController : MonoBehaviour
    {
        [Title("Ass")]
        [SerializeField] private int initialAssIndex;
        [SerializeField] private List<GameObject> assList = new();
        [ReadOnly] [SerializeField] private int currentAssIndex;

        [Title("Head")]
        [SerializeField] private int initialHeadIndex;
        [SerializeField] private List<GameObject> headList = new();
        [ReadOnly] [SerializeField] private int currentHeadIndex;

        private void Start()
        {
            currentAssIndex = initialAssIndex;
            currentHeadIndex = initialHeadIndex;
            foreach (var ass in assList)
                ass.SetActive(false);
            foreach (var head in headList)
                head.SetActive(false);
            assList[currentAssIndex].SetActive(true);
            headList[currentHeadIndex].SetActive(true);
        }

        public void NextAss()
        {
            assList[currentAssIndex].SetActive(false);
            currentAssIndex = currentAssIndex < assList.Count - 1 ? currentAssIndex++ : 0;
            assList[currentAssIndex].SetActive(true);
        }

        public void NextHead()
        {
            headList[currentHeadIndex].SetActive(false);
            currentHeadIndex = currentHeadIndex < headList.Count - 1 ? currentHeadIndex++ : 0;
            headList[currentHeadIndex].SetActive(true);
        }
    }
}