using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField]
    private PickUpInfo pickUpInfo = null;
    public PickUpInfo PickUpInfo { get => pickUpInfo; private set => pickUpInfo = value; }

    protected virtual void Collide2D(Collision2D collision) { }
    protected virtual void Trigger2D(Collider2D collision) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collide2D(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger2D(collision);
    }
}
