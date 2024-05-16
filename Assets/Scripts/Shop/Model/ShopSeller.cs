using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Seller")]
public class ShopSeller : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite avatar;
    [SerializeField] string description;
    [SerializeField] string[] salesPhrases;

    public string GetName() => name;
    public Sprite GetAvatar() => avatar;
    public string GetDescription() => description;
    public string GetRandomSalesPhrase() {
        return salesPhrases[Random.Range(0, salesPhrases.Length)];
    }
}