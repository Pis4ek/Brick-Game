using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializeBoolMatrixAttribute))]
public class CustomSerialization : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //int sizeX = property.FindPropertyRelative("sizeX").intValue;
        //int sizeY = property.FindPropertyRelative("sizeY").intValue;
        //BoolMatrix2D boolMatrix2D = property.objectReferenceValue as BoolMatrix2D;
        //SerializedPropertyType propertyType = property.FindPropertyRelative("").type;

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, label);

        Rect rectSizeY = new Rect(position.x,position.y, position.width - 200, EditorGUIUtility.singleLineHeight);
        position.y += EditorGUIUtility.singleLineHeight;
        Rect rectSizeX = new Rect(position.x, position.y, position.width - 200, EditorGUIUtility.singleLineHeight);
        //EditorGUI.IntField(rectSizeY, sizeY);
        //EditorGUI.IntField(rectSizeX, sizeX);
        //EditorGUI.TextField(rectSizeY, boolMatrix2D.sizeX.ToString());
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Высота элементов интерфейса
        return EditorGUIUtility.singleLineHeight * 4;
    }
}
