using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    public void MaxHealthChanged(float value)
    {
        healthBar.maxValue = value;
    }
    public void HealthChanged(float value)
    {
        healthBar.value = value;
    }
}
