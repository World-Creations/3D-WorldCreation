using System.Collections.Generic;
using UnityEngine;

public class Generic : MonoBehaviour
{
    [SerializeField] DialogueGraph graph;

    private void OnTriggerEnter(Collider other)
    {
        DialogueHandler.Instance.StartConversation(graph);
    }
}
