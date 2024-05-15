using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using static PlayerMovement;

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
    public const int maxHairCount = 64;

    public const string hairSpritePrefix = "Shirts_";


    [SerializeField, Range(0, maxHairCount)] private int shirtIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas shirtSpriteAtlas;
    [SerializeField] private ShirtsOffset offsets;


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
                lastSelectedAccIndex = realIndex;
                stringBuilder.Append(lastSelectedAccIndex);

                return stringBuilder.ToString();
            }
            else
            {
                lastSelectedAccIndex = realIndex + maxColumnCount * 3;
                stringBuilder.Append(lastSelectedAccIndex);

                return stringBuilder.ToString();
            }
        }

        if (direction.x != 0)
        {
            lastSelectedAccIndex = realIndex + maxColumnCount;
            stringBuilder.Append(lastSelectedAccIndex);

            return stringBuilder.ToString();
        }

        stringBuilder.Append(lastSelectedAccIndex);

        return stringBuilder.ToString();
    }
}
