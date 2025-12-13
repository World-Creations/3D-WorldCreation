using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class FishNode : Node {
    [SpritePreview(128)] public Sprite sprite;
    public int chance;
    public float resilience;
    public float progressModifier;
    public int value;

    public string GetName()
    {
        return sprite.name;
    }
}