using MEET_AND_TALK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static ShopController;

[RequireComponent(typeof(CanvasGroup))]
public class ShopView : MonoBehaviour
{
    [SerializeField] ShopNPCContainerView npcContainerView;
    [SerializeField] ShopViewItemContainer itemContainerView;
    [SerializeField] DialogueContainerSO dialogueObject;

    public UnityEvent<ShopItem> OnPurchased;
    public UnityEvent OnClosed;

    private ShopCollection shopCollection;
    private CanvasGroup canvasGroup;
    private CanPurchaseItem canPurchaseItem;
    private CouldNotPurchaseItem couldNotPurchaseItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(ShopCollection shopCollection, CanPurchaseItem canPurchaseItem, CouldNotPurchaseItem couldNotPurchaseItem)
    {
        this.shopCollection = shopCollection;
        this.canPurchaseItem = canPurchaseItem;
        this.couldNotPurchaseItem = couldNotPurchaseItem;

        Initialize();
    }
    public void Close()
    {
        OnClosed?.Invoke();
        CloseWindow();
    }

    void Initialize()
    {
        npcContainerView.Setup(shopCollection.GetSeller());
        itemContainerView.Setup(shopCollection.GetItems(), canPurchaseItem, couldNotPurchaseItem);
        itemContainerView.onPurchase.AddListener(OnItemPurchased);

        OpenWindow();
    }

    void CloseWindow()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void OpenWindow()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void OnItemPurchased(ShopItem shopItem)
    {
        Debug.Log("Purchased item!");

        UpdateDialogue();

        DialogueManager.Instance.StartDialogue(dialogueObject);
    }

    void UpdateDialogue()
    {
        dialogueObject.DialogueNodeDatas.First().TextType[0].LanguageGenericType = shopCollection.GetSeller().GetRandomSalesPhrase();
    }
}