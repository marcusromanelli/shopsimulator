using System;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class ShirtsAnimation : MonoBehaviour
{
    [Serializable]
    struct ShirtsOffset
    {
        public Vector2 upOffset;
        public Vector2 downOffset;
        public Vector2 rightOffset;
    }

    public const int maxColumnCount = 16;
    public const int maxShirtsCount = 64;

    public const string hairSpritePrefix = "Shirts_";


    [SerializeField, Range(0, maxShirtsCount)] private int shirtIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas shirtSpriteAtlas;
    [SerializeField] private ShirtsOffset offsets;


    private int lastSelectedShirtIndex = -1;

    Vector2 lastDirection;
    private void Awake()
    {
        playerMovement.onPlayerMoved += OnPlayerMoved;
    }

    void OnPlayerMoved(Vector2 direction)
    {
        lastDirection = direction;

        LoadShirt();
    }

    public void EquipItem(ShopItem item)
    {
        if (item == null)
            shirtIndex = -1;
        else
            shirtIndex = item.GetInternalId();

        LoadShirt();
    }
    void LoadShirt()
    {
        var spriteName = GetSpriteName(lastDirection, shirtIndex);

        spriteRenderer.sprite = shirtSpriteAtlas.GetSprite(spriteName);

        UpdateOffsets(lastDirection);
    }
    void UpdateOffsets(Vector2 direction)
    {
        if (direction.y != 0)
        {
            if (direction.y < 0)
            {

                transform.localPosition = offsets.downOffset;
            }
            else
            {
                transform.localPosition = offsets.upOffset;
            }
        }

        if (direction.x != 0)
        {
            transform.localPosition = offsets.rightOffset;
        }
    }

    string GetSpriteName(Vector2 direction, int spriteIndex)
    {
        StringBuilder stringBuilder = new StringBuilder(hairSpritePrefix);

        var realIndex = spriteIndex + Mathf.FloorToInt((float)spriteIndex / (float)maxColumnCount) * (3 * maxColumnCount);


        if (direction.y != 0)
        {
            if (direction.y < 0)
            {
                lastSelectedShirtIndex = realIndex;
                stringBuilder.Append(lastSelectedShirtIndex);

                return stringBuilder.ToString();
            }
            else
            {
                lastSelectedShirtIndex = realIndex + maxColumnCount * 3;
                stringBuilder.Append(lastSelectedShirtIndex);

                return stringBuilder.ToString();
            }
        }

        if (direction.x != 0)
        {
            lastSelectedShirtIndex = realIndex + maxColumnCount;
            stringBuilder.Append(lastSelectedShirtIndex);

            return stringBuilder.ToString();
        }

        stringBuilder.Append(lastSelectedShirtIndex);

        return stringBuilder.ToString();
    }
}
