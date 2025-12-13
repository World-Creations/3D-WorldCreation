using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class DialogueNodeBase : CoreNodeBase {
    [TextArea] public string dialogueSpoken;

    public abstract string getDialogueType{ get; }
}