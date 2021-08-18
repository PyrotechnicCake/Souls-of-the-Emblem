using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pyro
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;

        public void Start()
        {
            slider = GetComponent<Slider>();
        }

        public void SetMaxStam(int maxStam)
        {
            slider.maxValue = maxStam;
            slider.value = maxStam;
        }

        public void SetCurrentStam(int currentStam)
        {
            slider.value = currentStam;
        }
    }
}
