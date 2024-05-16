using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Collection")]
public class ShopCollection : ScriptableObject
{
    [SerializeField] ShopSeller seller;
    [SerializeField] ShopItem[] items;
}