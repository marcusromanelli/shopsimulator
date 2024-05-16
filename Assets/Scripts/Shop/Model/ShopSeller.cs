using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Seller")]
public class ShopSeller : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite avatar;
    [SerializeField] string description;
    [SerializeField] string[] salesPhrases;
}