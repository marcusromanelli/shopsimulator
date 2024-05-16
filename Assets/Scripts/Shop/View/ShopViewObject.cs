using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopViewObject : MonoBehaviour, IPoolable
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemValue;
    [SerializeField] Image currencyIcon;
    [SerializeField] Button button;

    public UnityEvent<ShopItem> OnClickPurchase;
    
    private ShopItem item;

    private void Awake()
    {
        button.onClick.AddListener(OnClickedPurchase);
    }
    public void Setup(ShopItem item)
    {
        itemImage.sprite = item.GetIcon();
        itemName.text = item.GetName();
        itemValue.text = item.GetCostData().amount.ToString();
        currencyIcon.sprite = item.GetCostData().currency.GetIcon();

        this.item = item;
    }

    void OnClickedPurchase()
    {
        this.OnClickPurchase?.Invoke(item);
    }

    public void Setup()
    {
        
    }

    public void OnEnabled()
    {
        
    }

    public void OnDisabled()
    {
        item = null;
        OnClickPurchase.RemoveAllListeners();
    }

    public void Destroy()
    {
        
    }
}