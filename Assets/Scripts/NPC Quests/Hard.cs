using System.Collections.Generic;
using UnityEngine;

public class Hard : MonoBehaviour
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
                objective = new Objective("CatchFish", "Catch a fish: {0}/{1}", 100);
                GameManager.Instance.AddObjective(objective);
                quest = new Quest("Hard Fishing", "This is worth it right?", new List<Objective>() { objective });
                GameManager.Instance.AddQuest(quest);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(quest.name);
                break;
        }
    }
}
