using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class PantsAnimation : MonoBehaviour
{
    //struct SpriteAnimationData
    //{
    //    AnimationClip upIdle;
    //    AnimationClip downIdle;
    //    AnimationClip rightIdle;
    //    AnimationClip upWalk;
    //    AnimationClip downWalk;
    //    AnimationClip rightWalk;
    //}

    public const int maxColumnCount = 120;
    public const int maxPantsCount = 64;

    public const string hairSpritePrefix = "Pants_";


    [SerializeField, Range(0, maxPantsCount)] private int pantsIndex;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAtlas pantsSpriteAtlas;
    [SerializeField] private Animation animation;
    [SerializeField] private Vector2 offset;


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
        Sprite[] spriteArray = new Sprite[2];
        spriteArray[0] = pantsSpriteAtlas.GetSprite(GetSpriteName(0 + 1));
        spriteArray[1] = pantsSpriteAtlas.GetSprite(GetSpriteName(0 + 2));

        /* var clip = new AnimationClip();
         var curve = AnimationCurve.Linear(1, 1, 3, 3);
         clip.SetCurve("", typeof(Transform), "localScale.y", curve);

         EditorCurveBinding spriteBinding = new EditorCurveBinding();
         spriteBinding.type = typeof(SpriteRenderer);
         spriteBinding.path = "";
         spriteBinding.propertyName = "m_Sprite";

         ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[3];


         spriteKeyFrames[0] = GenerateFrame(0f, spriteArray[0]);
         spriteKeyFrames[1] = GenerateFrame(0.1f, spriteArray[1]);
         spriteKeyFrames[2] = GenerateFrame(0.2f, spriteArray[0]);


         AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);


         clip.name = "Move"; // set name
         clip.legacy = true; // change to legacy
 */

        var clip = CreateClip(spriteArray, "Derp");

        animation.clip = clip; // set default clip
        animation.AddClip(clip, clip.name); // add clip to animation component

        //AssetDatabase.CreateAsset(clip, "Assets/" + clip.name + ".anim"); // to create asset
        animation.Play(); // then play

        //var idleSpriteIndex = GetIdleSpriteRealIndex(lastDirection, pantsIndex);
        //var animationData = GetSpriteAnimationData(idleSpriteIndex);

        //animation.AddClip(animationData, "Test");
        //animation.clip = animationData;
        //animation.Play("Test");

        UpdateOffset();
    }

    public AnimationClip CreateClip(Sprite[] sprites, string clipName)
    {
        // Output nothing if there is no clip name
        if (string.IsNullOrEmpty(clipName))
        {
            return null;
        }

        // Could be inputs
        int sampleRate = 12;
        bool isLooping = false;

        // Create a new Clip
        AnimationClip clip = new AnimationClip();

        // Apply the name and framerate
        clip.name = clipName;
        clip.frameRate = sampleRate;
        clip.legacy = true;

        // Apply Looping Settings
        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = isLooping;
        AnimationUtility.SetAnimationClipSettings(clip, clipSettings);

        // Initialize the curve property for the animation clip
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.propertyName = "m_Sprite";
        // Assumes user wants to apply the sprite property to the root element
        curveBinding.path = "";
        curveBinding.type = typeof(SpriteRenderer);

        // Build keyframes for the property using the supplied Sprites
        ObjectReferenceKeyframe[] keys = CreateKeysForSprites(sprites, sampleRate);

        // Build the clip if valid
        if (keys.Length > 0)
        {
            // Set the keyframes to the animation
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keys);
        }

        return clip;
    }


    private ObjectReferenceKeyframe[] CreateKeysForSprites(Sprite[] sprites, int samplesPerSecond)
    {
        List<ObjectReferenceKeyframe> keys = new List<ObjectReferenceKeyframe>();
        float timePerFrame = 1.0f / samplesPerSecond;
        float currentTime = 0.0f;
        foreach (Sprite sprite in sprites)
        {
            ObjectReferenceKeyframe keyframe = new ObjectReferenceKeyframe();
            keyframe.time = currentTime;
            keyframe.value = sprite;
            keys.Add(keyframe);

            currentTime += timePerFrame;
        }

        return keys.ToArray();
    }




    AnimationClip GetSpriteAnimationData(int realSpriteIndex)
    {
        if (spriteAnimationCache.ContainsKey(realSpriteIndex))
        {
            return spriteAnimationCache[realSpriteIndex];
        }

        spriteAnimationCache[realSpriteIndex] = GenerateAnimationData(realSpriteIndex);

        return spriteAnimationCache[realSpriteIndex];
    }
    AnimationClip GenerateAnimationData(int realSpriteIndex)
    {
        Sprite[] spriteData = new Sprite[2];
        spriteData[0] = pantsSpriteAtlas.GetSprite(GetSpriteName(realSpriteIndex + 1));
        spriteData[1] = pantsSpriteAtlas.GetSprite(GetSpriteName(realSpriteIndex + 2));

        return GenerateAnimationClip(spriteData);
    }
    void UpdateOffset()
    {
        transform.localPosition = offset;
    }

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

    int GetIdleSpriteRealIndex(Vector2 direction, int spriteIndex)
    {
        var realIndex = (spriteIndex + ((spriteIndex % maxColumnCount) * 3)) + Mathf.FloorToInt((float)spriteIndex / (float)maxColumnCount) * (3 * maxColumnCount);


        if (direction.y != 0)
        {
            if (direction.y < 0)
            {
                lastSelectedAccIndex = realIndex;

                return realIndex;
            }
            else
            {
                lastSelectedAccIndex = realIndex + maxColumnCount * 2;

                return realIndex;
            }
        }

        if (direction.x != 0)
        {
            lastSelectedAccIndex = realIndex + maxColumnCount;

            return realIndex;
        }

        return lastSelectedAccIndex;
    }
    string GetSpriteName(int realSpriteIndex)
    {
        StringBuilder stringBuilder = new StringBuilder(hairSpritePrefix);

        stringBuilder.Append(realSpriteIndex);

        return stringBuilder.ToString();
    }
}
