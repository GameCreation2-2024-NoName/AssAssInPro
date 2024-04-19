using System;
using HighlightPlus2D;
using PurpleFlowerCore;
using UnityEngine;

namespace Test
{
    public class TestHighLight : MonoBehaviour
    {
        private void Start()
        {
            HighlightEffect2D effect = GetComponent<HighlightEffect2D>();
            effect.highlighted = true;
            PFCLog.Info("testHigh");
        }
    }
}