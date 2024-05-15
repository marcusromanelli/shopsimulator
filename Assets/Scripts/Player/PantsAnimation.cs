using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class PantsAnimation : MonoBehaviour
{
    [Serializable]
    struct SpriteAnimationData
    {
        public AnimationClip upIdle;
        public AnimationClip downIdle;
        public AnimationClip rightIdle;
        public AnimationClip upWalk;
        public AnimationClip downWalk;
        public AnimationClip rightWalk;
    }
    enum SpriteDirection
    {
        Up,
        Down,
        Right
    }
    enum SpriteState
    {
        Idle,
        Walking
    }
    public const int maxColumnCount = 120;
    public const int maxPantsCount = 64;

    public const string hairSpritePrefix = "Pants_";


    [SerializeField, Range(0, maxPantsCount)] private int pantsIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas pantsSpriteAtlas;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 offset;
    [SerializeField] private SpriteAnimationData animationData;


    private int lastSelectedAccIndex;
    Dictionary<int, AnimationClip> spriteAnimationCache = new Dictionary<int, AnimationClip>();

    Vector2 lastDirection;
    private void Awake()
    {
        playerMovement.onPlayerMoved += OnPlayerMoved;
    }

    void OnPlayerMoved(Vector2 direction)
    {
        lastDirection = direction;

        LoadPants();
    }

    void LoadPants()
    {
        GenerateFullAnimationData();

        //UpdateOffset();
    }

    void GenerateFullAnimationData()
    {
        animationData.upIdle.ClearCurves();

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[3];

        Sprite[] spriteArray = new Sprite[2];
        spriteArray[0] = pantsSpriteAtlas.GetSprite(GetSpriteName(0 + 1));
        spriteArray[1] = pantsSpriteAtlas.GetSprite(GetSpriteName(0 + 2));

        spriteKeyFrames[0] = GenerateFrame(0f, spriteArray[0]);
        spriteKeyFrames[1] = GenerateFrame(0.1f, spriteArray[1]);
        spriteKeyFrames[2] = GenerateFrame(0.2f, spriteArray[0]);

///        AnimationUtility.SetObjectReferenceCurve(animationData.upIdle, spriteBinding, spriteKeyFrames);
    }

    /*int GetSpriteRealIndex(int itemIndex, SpriteState spriteState, SpriteDirection spriteDirection)
    {
        var realIndex = (itemIndex + ((itemIndex % maxColumnCount) * 3)) + Mathf.FloorToInt((float)itemIndex / (float)maxColumnCount) * (3 * maxColumnCount);

        switch (spriteDirection)
        {
            case SpriteDirection.Up:
                switch (spriteState)
                {
                    case SpriteState.Idle:
                        return realIndex + maxColumnCount * 2;
                    case SpriteState.Walking:
                        return realIndex + maxColumnCount * 2;

                }
                break;
        }

        if (direction.y != 0)
        {
            if (direction.y < 0)
            {
            }
            else
            {
                lastSelectedAccIndex = 

                return realIndex;
            }
        }

        if (direction.x != 0)
        {
            lastSelectedAccIndex = realIndex + maxColumnCount;

            return realIndex;
        }

        return lastSelectedAccIndex;

    }*/

    //AnimationClip GetSpriteAnimationData(int realSpriteIndex)
    //{
    //    if (spriteAnimationCache.ContainsKey(realSpriteIndex))
    //    {
    //        return spriteAnimationCache[realSpriteIndex];
    //    }

    //    spriteAnimationCache[realSpriteIndex] = GenerateAnimationData(realSpriteIndex);

    //    return spriteAnimationCache[realSpriteIndex];
    //}
    //AnimationClip GenerateAnimationData(int realSpriteIndex)
    //{
    //    Sprite[] spriteData = new Sprite[2];
    //    spriteData[0] = pantsSpriteAtlas.GetSprite(GetSpriteName(realSpriteIndex + 1));
    //    spriteData[1] = pantsSpriteAtlas.GetSprite(GetSpriteName(realSpriteIndex + 2));

    //    return GenerateAnimationClip(spriteData);
    //}
    //void UpdateOffset()
    //{
    //    transform.localPosition = offset;
    //}

    ObjectReferenceKeyframe GenerateFrame(float time, Sprite sprite)
    {
        ObjectReferenceKeyframe frame = new ObjectReferenceKeyframe
        {
            time = time,
            value = sprite
        };
        return frame;
    }
    AnimationClip GenerateAnimationClip(Sprite[] spriteArray)
    {
        AnimationClip animClip = new AnimationClip();
        animClip.frameRate = 30;   // FPS

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[3];

        spriteKeyFrames[0] = GenerateFrame(0f, spriteArray[0]);
        spriteKeyFrames[1] = GenerateFrame(0.1f, spriteArray[1]);
        spriteKeyFrames[2] = GenerateFrame(0.2f, spriteArray[0]);


        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

        animClip.legacy = true;


        AssetDatabase.CreateAsset(animClip, "Assets/Test.anim");
        AssetDatabase.SaveAssets();


        return animClip;
    }

    //int GetIdleSpriteRealIndex(Vector2 direction, int spriteIndex)
    //{
    //    var realIndex = (spriteIndex + ((spriteIndex % maxColumnCount) * 3)) + Mathf.FloorToInt((float)spriteIndex / (float)maxColumnCount) * (3 * maxColumnCount);


    //    if (direction.y != 0)
    //    {
    //        if (direction.y < 0)
    //        {
    //            lastSelectedAccIndex = realIndex;

    //            return realIndex;
    //        }
    //        else
    //        {
    //            lastSelectedAccIndex = realIndex + maxColumnCount * 2;

    //            return realIndex;
    //        }
    //    }

    //    if (direction.x != 0)
    //    {
    //        lastSelectedAccIndex = realIndex + maxColumnCount;

    //        return realIndex;
    //    }

    //    return lastSelectedAccIndex;
    //}
    string GetSpriteName(int realSpriteIndex)
    {
        StringBuilder stringBuilder = new StringBuilder(hairSpritePrefix);

        stringBuilder.Append(realSpriteIndex);

        return stringBuilder.ToString();
    }
}
