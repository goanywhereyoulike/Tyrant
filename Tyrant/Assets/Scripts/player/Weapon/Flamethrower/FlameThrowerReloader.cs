using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerReloader : MonoBehaviour
{
    private float currentAmmo;
    private float maxAmmo;

    bool firstStart = false;

    public float CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public float MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public bool FirstStart { get => firstStart; set => firstStart = value; }

    private void Start()
    {
        StartCoroutine(RegenAmmo());
    }

    private IEnumerator RegenAmmo()
    {
        while (true)
        {
            if (CurrentAmmo >= MaxAmmo)
            {
                FirstStart = false;
                yield return null;
            }

            if (CurrentAmmo < MaxAmmo && FirstStart == false)
            {
                FirstStart = true;
                yield return new WaitForSeconds(0.5f);
            }

            if (FirstStart)
            {
                CurrentAmmo += MaxAmmo / 100;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
