using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] int price;
    [SerializeField] string name;

    public Sprite GetIcon() => icon;
    public float GetPrice() => price;
    public string GetName() => name;
}
