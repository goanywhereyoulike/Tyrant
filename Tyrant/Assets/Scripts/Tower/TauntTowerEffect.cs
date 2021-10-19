using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TauntTowerEffect : MonoBehaviour
{
    float duration = 1.5f;
    private float t = 0;

    public TowerTemplate towerData;
    [SerializeField]
    Slider ColdDown;
    public bool IsCoolDown = false;

    private float EffectDuration = 10;
    private float Effecttime;
    private float CoolDownSpeed;

    private void Start()
    {
        Effecttime = 0.0f;
        EffectDuration = towerData.BulletLimit;
        CoolDownSpeed = towerData.CoolDownSpeed;
        ColdDown.maxValue = EffectDuration;
        ColdDown.value = float.IsNaN(Effecttime) ? 0f : Effecttime;
        ColdDown.fillRect.transform.GetComponent<Image>().color = Color.yellow;
    }

    void Update()
    {
        if (IsCoolDown)
        {
            Color color = Color.black;
            color.a = -10;
            GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", 0.03f);
            GetComponent<SpriteRenderer>().material.SetColor("_Color", color);


            ColdDown.fillRect.transform.GetComponent<Image>().color = Color.red;

            Effecttime -= CoolDownSpeed * Time.deltaTime;
            ColdDown.value = Effecttime;

            if (Effecttime <= 0)
            {
                Effecttime = 0;
                ColdDown.value = Effecttime;
                ColdDown.fillRect.transform.GetComponent<Image>().color = Color.yellow;
                IsCoolDown = false;
            }


        }
        if (!IsCoolDown)
        {
            ColorChanger();
        }


    }

    void ColorChanger()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", 0.03f);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.Lerp(Color.yellow, Color.red, t));

        if (t < 1)
        {
            t += Time.deltaTime / duration;
        }
        else
        {
            t = 0;
        }


        Effecttime += Time.deltaTime * 0.5f;
        ColdDown.value = Effecttime;

        if (Effecttime >= EffectDuration)
        {
            IsCoolDown = true;
        }


    }
}
