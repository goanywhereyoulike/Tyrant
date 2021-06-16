using UnityEngine;

[System.Serializable]
public class PickUpInfo
{
    public enum Category
    {
        Potion,
        Weapon
    }

    [SerializeField]
    private string name = string.Empty;
    [SerializeField]
    private Category category = Category.Potion;
    [SerializeField]
    private string description = string.Empty;

    public string PickUpName { get => name; }
    public Category PickUpCategory { get => category; }
    public string PickUpDescription { get => description; }
}
