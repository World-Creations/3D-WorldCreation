using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EntryNode : CoreNodeBase {
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool exit;
}