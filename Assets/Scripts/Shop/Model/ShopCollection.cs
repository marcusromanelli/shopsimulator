using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using static ShopController;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] int price;
    [SerializeField] string name;
    [SerializeField] ItemType type;
    [SerializeField] int itemId;
    [SerializeField] GUID uniqueId = new GUID();

    public Sprite GetIcon() => icon;
    public float GetPrice() => price;
    public string GetName() => name;
}
