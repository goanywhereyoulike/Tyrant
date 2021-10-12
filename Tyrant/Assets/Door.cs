using UnityEngine;

public class Door : MonoBehaviour
{
    public int roomID;
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField]
    private bool isBossDoor;
    public bool IsBossDoor { get=> isBossDoor; }
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
