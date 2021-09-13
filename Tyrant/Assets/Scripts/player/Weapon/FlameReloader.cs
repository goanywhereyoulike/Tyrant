using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameReloader : MonoBehaviour
{
    public Slider ammoBar;
    public float currentAmmo { get; set; }
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine ammoRegen;
    [SerializeField]
    private FlamethrowerStates flamethrowerStates = null;

    // Start is called before the first frame update
    void Start()
    {
        ammoBar.gameObject.SetActive(false);

        ammoBar.maxValue = flamethrowerStates.MaxAmmo;
        currentAmmo = flamethrowerStates.MaxAmmo;
        ammoBar.value = flamethrowerStates.MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoRegen != null)
        {
            StopCoroutine(ammoRegen);
        }
        ammoRegen = StartCoroutine(RegenAmmo());
    }
    private IEnumerator RegenAmmo()
    {
        yield return new WaitForSeconds(0.5f);
        while (currentAmmo < flamethrowerStates.MaxAmmo)
        {
            currentAmmo += flamethrowerStates.MaxAmmo / 100;
            ammoBar.value = currentAmmo;
            yield return regenTick;
        }
        ammoRegen = null;
    }
}
