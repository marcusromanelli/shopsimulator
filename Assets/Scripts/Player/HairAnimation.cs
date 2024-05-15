using System.Text;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class HairAnimation : MonoBehaviour
{
    public const int maxHairCount = 16;
    public const string hairSpritePrefix = "Hairs_";


    [SerializeField, Range(0, maxHairCount)] private int hairIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas hairSpriteAtlas;


    private int lastSelectedHairIndex;

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
        var spriteName = GetSpriteName(lastDirection, hairIndex);

        spriteRenderer.sprite = hairSpriteAtlas.GetSprite(spriteName);
    }
    string GetSpriteName(Vector2 direction, int hairIndex)
    {
        StringBuilder stringBuilder = new StringBuilder(hairSpritePrefix);
        //Down hair = hairIndex
        //Right hair = hairIndex + (maxHairCount)
        //Up hair = hairIndex + (maxHairCount * 2)

        if (direction.y != 0)
        {
            if(direction.y > 0)
            {
                lastSelectedHairIndex = hairIndex + (maxHairCount * 2);
            }
            else
            {
                lastSelectedHairIndex = hairIndex;
            }
            stringBuilder.Append(lastSelectedHairIndex);

            return stringBuilder.ToString();
        }


        if(direction.x != 0)
        {
            lastSelectedHairIndex = hairIndex + (maxHairCount);

            stringBuilder.Append(lastSelectedHairIndex);

            return stringBuilder.ToString();
        }

        stringBuilder.Append(lastSelectedHairIndex);

        return stringBuilder.ToString();
    }
}
