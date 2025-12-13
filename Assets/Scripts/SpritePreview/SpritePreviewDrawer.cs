using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
public class SpritePreviewDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SpritePreviewAttribute preview = (SpritePreviewAttribute)attribute;
        return preview.height + EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SpritePreviewAttribute preview = (SpritePreviewAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        // Draw object field
        Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect, property, label);

        // Draw preview below the field (only if sprite exists)
        Sprite sprite = property.objectReferenceValue as Sprite;
        if (sprite)
        {
            Rect previewRect = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight + 4,
                preview.height,
                preview.height
            );

            Texture2D texture = sprite.texture;
            GUI.DrawTexture(previewRect, texture, ScaleMode.ScaleToFit);
        }

        EditorGUI.EndProperty();
    }
}
