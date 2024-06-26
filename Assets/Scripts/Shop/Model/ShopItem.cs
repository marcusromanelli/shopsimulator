using System;
using UnityEngine;
using static ShopController;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ScriptableObject, IIdentifiable
{
    [Serializable]
    public struct Cost
    {
        public CurrencyData currency;
        public int amount;
    }
    [SerializeField] Sprite icon;
    [SerializeField] Cost price;
    [SerializeField] string name;
    [SerializeField] ItemType type;
    [SerializeField] string id = Guid.NewGuid().ToString();
    [SerializeField] int internalItemId;

    public Sprite GetIcon() => icon;
    public Cost GetCostData() => price;
    public string GetName() => name;
    public string GetId() => id;
    public ItemType GetType() => type;
    public int GetInternalId() => internalItemId;
}
