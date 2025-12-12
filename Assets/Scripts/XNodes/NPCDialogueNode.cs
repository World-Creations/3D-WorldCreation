using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NPCDialogueNode : DialogueNodeBase
{
    [Input(typeConstraint = TypeConstraint.Strict)] public bool entry;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool exit;

    public override string getDialogueType { get { return "NPC"; } }
}