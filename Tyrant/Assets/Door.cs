using UnityEngine;

public class Door : MonoBehaviour
{
    public int roomID;
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.SetBool("IsClose", false);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
