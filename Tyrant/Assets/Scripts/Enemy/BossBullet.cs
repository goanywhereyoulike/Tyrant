using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float damagetoTower;
    [SerializeField]
    private float damagetoCannonTower;
    [SerializeField]
    private float damagetoChainTower;

    public float bulletSpeed;
    float a;
    float b;
    float distance;
    float range;
    float speed;
    Vector2 shootPosition;
    Vector2 targetPosition;
    Vector3 direction;
    public Vector2 Position { get => targetPosition; set => targetPosition = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    private void Start()
    {
        shootPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        a = shootPosition.x - transform.position.x;
        b = shootPosition.y - transform.position.y;
        distance = Mathf.Sqrt((a * a) + (b * b));
        if ((Vector2)transform.position == Position)
        {
            gameObject.SetActive(false);
        }
        speed = bulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Position, speed);
        //transform.position += direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "Tower")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoTower);
        }
        if (collider.gameObject.tag == "ChainTower")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoChainTower);
            gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "CannonTower")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoCannonTower);
            gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Base")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        //gameObject.SetActive(false);
    }
}
