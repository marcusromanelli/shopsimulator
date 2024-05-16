using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, IPoolable
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button button;

    public UnityEvent<ShopItem> OnClicked;

    private ShopItem shopItem;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        OnClicked?.Invoke(shopItem);
    }
    public void Destroy()
    {
        
    }

    public void OnDisabled()
    {
        OnClicked.RemoveAllListeners();
    }

    public void OnEnabled()
    {
        
    }

    public void Setup(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        itemImage.sprite = shopItem.GetIcon();
    }

    public void Setup()
    {
        
    }
}
