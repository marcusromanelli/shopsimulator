using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] int price;
    [SerializeField] string name;
}
