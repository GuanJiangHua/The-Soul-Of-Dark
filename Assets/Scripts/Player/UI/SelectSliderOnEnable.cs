using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class SelectSliderOnEnable : MonoBehaviour
    {
        public Button statsButton;
        private void OnEnable()
        {
            statsButton.Select();
            statsButton.OnSelect(null);
        }
    }
}