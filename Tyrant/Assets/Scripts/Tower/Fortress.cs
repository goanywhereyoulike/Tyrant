using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fortress : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update

    public GameObject PlayerRecoverEffectPrefab;
    public GameObject DestroyedBase;

    [SerializeField]
    float RecoverRange = 1.8f;

    [SerializeField]
    private float health;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                GameOver();

            }
        }
        get
        {
            return health;

        }
    }

    [SerializeField]
    private Slider Healthbar;

    [SerializeField]
    float RecoverRate;
    float WaitFire = 0.0f;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        health = 100;
        Healthbar.maxValue = Health;
        Healthbar.value = Health;
        RecoverRange = 1.8f;

    }

    // Update is called once per frame
    void Update()
    {

        float distance = (player.transform.position - transform.position).magnitude;
        


        if (distance < RecoverRange * RecoverRange)
        {
            WaitFire += Time.deltaTime;
            if (WaitFire > RecoverRate) //Fires gun everytime timer exceeds firerate
            {
                GameObject effect = Instantiate(PlayerRecoverEffectPrefab, player.transform.position, Quaternion.identity);
                Destroy(effect, 0.1f);

                player.HealthRecover(1.0f);
                WaitFire = 0.0f;
            }
        }

    }

    void GameOver()
    {
        Destroy(gameObject.transform.gameObject);
        Instantiate(DestroyedBase, transform.position, transform.rotation);

    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Healthbar.value = Health;
    }

}
