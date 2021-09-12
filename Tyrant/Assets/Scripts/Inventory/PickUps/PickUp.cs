using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField]
    private PickUpInfo pickUpInfo = null;
    public PickUpInfo PickUpInfo { get => pickUpInfo; private set => pickUpInfo = value; }

    public bool CanBePicked { get; protected set; }
    protected virtual void Collide2DEnter(Collision2D collision) { }
    protected virtual void Collide2DStay(Collision2D collision) { }
    protected virtual void Trigger2DEnter(Collider2D collision) { }
    protected virtual void Trigger2DStay(Collider2D collision) { }
    protected virtual void Trigger2DExit(Collider2D collision) { }

    private void Awake()
    {
        CanBePicked = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collide2DEnter(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Collide2DStay(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanBePicked = false;
        }
            Trigger2DExit(collision); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanBePicked = true;
        }
        Trigger2DEnter(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Trigger2DStay(collision);
    }
}
