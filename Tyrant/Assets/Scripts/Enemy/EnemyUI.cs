using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Slider armorBar;

    public void MaxHealthChanged(float value)
    {
        if (!healthBar)
            return;
        healthBar.maxValue = value;
    }
    public void HealthChanged(float value)
    {
        if (!healthBar)
            return;
        healthBar.value = value;
    }
    public void MaxArmorChanged(float value)
    {
        if (!armorBar)
            return;
        armorBar.maxValue = value;
    }
    public void ArmorChanged(float value)
    {
        if (!armorBar)
            return;
        armorBar.value = value;
    }

    public void ShutdownArmorBar()
    {
        armorBar.gameObject.SetActive(false);
    }
}
