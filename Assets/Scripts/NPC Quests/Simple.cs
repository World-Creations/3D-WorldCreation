using System.Collections.Generic;
using UnityEngine;

public class Simple : MonoBehaviour
{
    [SerializeField] DialogueGraph graph;

    private Objective objective;
    private Quest quest;

    private void OnTriggerEnter(Collider other)
    {
        DialogueHandler.Instance.StartConversation(graph, ConversationEnd);
    }

    private void ConversationEnd(string result)
    {
        switch (result)
        {
            case "Accepted":
                objective = new Objective("CatchFish", "Catch a fish: {0}/{1}", 12);
                GameManager.Instance.AddObjective(objective);
                quest = new Quest("Simple Fishing", "That guy was kinda rude.", new List<Objective>() { objective });
                GameManager.Instance.AddQuest(quest);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(quest.name);
                break;
        }
    }
}
