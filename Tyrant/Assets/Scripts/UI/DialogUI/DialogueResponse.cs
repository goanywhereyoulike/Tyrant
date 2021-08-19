using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueResponse
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickResponse;

    public UnityEvent OnPickResponse => onPickResponse;
}
