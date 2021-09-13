using UnityEngine;

public class BossBallsBullet : MonoBehaviour
{
    private int damage;
    float range;
    public float stayTime;
    float timeCheck;
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    private void Start()
    {
        timeCheck = stayTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeCheck < 0f)
        {
            gameObject.SetActive(false);
        }
        timeCheck -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Base")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(Damage);
            gameObject.SetActive(false);
            Debug.Log("attack");
        }
    }
}
