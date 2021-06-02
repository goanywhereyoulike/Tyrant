using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField]
    private PickUpInfo pickUpInfo = null;
    public PickUpInfo PickUpInfo { get => pickUpInfo; private set => pickUpInfo = value; }

    protected virtual void Collide(Collision collision) { }
    protected virtual void trigger(Collider other) { }

    private void OnCollisionEnter(Collision collision)
    {
        Collide(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        trigger(other);
    }
}
