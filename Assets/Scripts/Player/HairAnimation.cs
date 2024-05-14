using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerMovement;

[ExecuteInEditMode]
public class HairAnimation : MonoBehaviour
{
    [SerializeField] private int hairIndex;
    [SerializeField] private AnimationClip animationClipReference;

    public bool Run;

    Vector2 lastDirection;
    private void Awake()
    {
    }

    void Update()
    {
        if (!Run)
            return;

        AnimationClip animClip = new AnimationClip();
        animClip.frameRate = 30;   // FPS

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

     //   ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
     //   for (int i = 0; i < (sprites.Length); i++)
     //   {
     //       spriteKeyFrames *= new ObjectReferenceKeyframe(); *
     //_ spriteKeyFrames *.time = i; _
     //spriteKeyFrames_.value = sprites;
     //   }


        Run = false;
    }
}
