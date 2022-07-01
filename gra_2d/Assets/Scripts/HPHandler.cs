using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    public Slider slider;

    public void setMaxHP(float maxHP){
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void setHP(float hp){
        slider.value = hp;
    }
}
