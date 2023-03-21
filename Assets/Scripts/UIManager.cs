using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NB
{
    public class UIManager : MonoBehaviour
    {
        public GameObject crossHair;

        public void SetCrossHairColor(Color color)
        {
            Image[] images = crossHair.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.color = color;
            }
        }
    }
}