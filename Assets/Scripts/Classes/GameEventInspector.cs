using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GameEvent))]
public class GameEventInspector : PropertyDrawer
{
    int res = 0;
    bool show = false;
    GameEvent.EventType type = 0;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 1;
        if (show)
            lines++;
        return lines*16+2*(lines-1);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect popupRect = new Rect(position.x, position.y, position.width, 16);
        show = EditorGUI.Foldout(popupRect, show, "a");
        if (show)
        {
            Rect eventTypeRect = new Rect(position.x, position.y+18, position.width, 16);
            EditorGUI.indentLevel++;
            type = (GameEvent.EventType)EditorGUI.EnumPopup(eventTypeRect, type);
            
            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
