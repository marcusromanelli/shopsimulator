using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Collection")]
public class ShopCollection : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] ShopItem[] items;
}