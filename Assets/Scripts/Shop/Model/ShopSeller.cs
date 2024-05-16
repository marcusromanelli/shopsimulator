using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Seller")]
public class ShopSeller : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite avatar;
    [SerializeField] string description;
    [SerializeField] string[] salesPhrases;
    [SerializeField] string[] notEnoughtMoneyPhrases;

    public string GetName() => name;
    public Sprite GetAvatar() => avatar;
    public string GetDescription() => description;
    public string GetRandomSalesPhrase()
    {
        return GetRandomPhrase(salesPhrases);
    }
    public string GetRandomNotEnoughtMoneyPhrase()
    {
        return GetRandomPhrase(notEnoughtMoneyPhrases);
    }

    string GetRandomPhrase(string[] array)
    {
        if (array.Length == 0)
            return "";

        return array[Random.Range(0, array.Length)];
    }
}