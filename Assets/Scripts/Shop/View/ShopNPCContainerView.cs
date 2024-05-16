using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPCContainerView : MonoBehaviour
{
    [SerializeField] Image avatar;
    [SerializeField] TMP_Text description;

    private ShopSeller seller;

    private void Awake()
    {
    }
    public void Setup(ShopSeller seller)
    {
        this.seller = seller;

        avatar.sprite = this.seller.GetAvatar();
        description.text = this.seller.GetDescription();
    }
}