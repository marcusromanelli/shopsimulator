using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using static PlayerMovement;

[ExecuteInEditMode]
public class AccessAnimation : MonoBehaviour
{
    public const int maxColumnCount = 8;
    public const int maxHairCount = 18;

    public const string hairSpritePrefix = "Accessories_";


    [SerializeField, Range(0, maxHairCount)] private int accessoryIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas accessorySpriteAtlas;


    private int lastSelectedAccIndex;

    Vector2 lastDirection;
    private void Awake()
    {
        playerMovement.onPlayerMoved += OnPlayerMoved;
    }

    void OnPlayerMoved(Vector2 direction)
    {
        lastDirection = direction;

        LoadHair();
    }

    void LoadHair()
    {
        var spriteName = GetSpriteName(lastDirection, accessoryIndex);

        spriteRenderer.sprite = accessorySpriteAtlas.GetSprite(spriteName);

    }
    string GetSpriteName(Vector2 direction, int spriteIndex)
    {
        StringBuilder stringBuilder = new StringBuilder(hairSpritePrefix);


        var realIndex = spriteIndex + Mathf.FloorToInt((float)spriteIndex / (float)maxColumnCount) * (maxColumnCount);

        if (direction.y != 0) {
            if (direction.y < 0)
            {
                lastSelectedAccIndex = realIndex;
                stringBuilder.Append(lastSelectedAccIndex);

                return stringBuilder.ToString();
            }
            else
            {
                return null;
            }
        }

        if(direction.x != 0)
        {
            lastSelectedAccIndex = realIndex + maxColumnCount;
            stringBuilder.Append(lastSelectedAccIndex);

            return stringBuilder.ToString();
        }

        stringBuilder.Append(lastSelectedAccIndex);

        return stringBuilder.ToString();
    }
}
