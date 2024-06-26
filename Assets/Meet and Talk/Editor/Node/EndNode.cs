using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MEET_AND_TALK
{
    public class EndNode : BaseNode
    {
        private EndNodeType endNodeType = EndNodeType.End;
        private EnumField enumField;
        private IntegerField returnValueField;
        private int returnValue = 0;

        public EndNodeType EndNodeType { get => endNodeType; set => endNodeType = value; }
        public int ReturnValue { get => returnValue; set => returnValue = value; }

        public EndNode()
        {

        }

        public EndNode(Vector2 _position, DialogueEditorWindow _editorWindow, DialogueGraphView _graphView)
        {
            editorWindow = _editorWindow;
            graphView = _graphView;

            title = "End";
            SetPosition(new Rect(_position, defualtNodeSize));
            nodeGuid = Guid.NewGuid().ToString();

            AddInputPort("Input", Port.Capacity.Multi);

            enumField = new EnumField()
            {
                value = endNodeType
            };

            enumField.Init(endNodeType);

            enumField.RegisterValueChangedCallback((value) =>
            {
                endNodeType = (EndNodeType)value.newValue;
            });
            enumField.SetValueWithoutNotify(endNodeType);

            mainContainer.Add(enumField);



            returnValueField = new IntegerField("");
            returnValueField.RegisterValueChangedCallback(value =>
            {
                returnValue = value.newValue;
            });
            returnValueField.SetValueWithoutNotify(returnValue);

            returnValueField.AddToClassList("ReturnValue");
            mainContainer.Add(returnValueField);
        }

        public override void LoadValueInToField()
        {
            enumField.SetValueWithoutNotify(endNodeType);
            returnValueField.SetValueWithoutNotify(returnValue);
        }
    }
}
