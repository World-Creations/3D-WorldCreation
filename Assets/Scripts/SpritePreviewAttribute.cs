using UnityEngine;

public class SpritePreviewAttribute : PropertyAttribute
{
    public float height;

    public SpritePreviewAttribute(float height = 64f)
    {
        this.height = height;
    }
}
