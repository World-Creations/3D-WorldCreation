using System.Collections.Generic;
using UnityEngine;

public class HungryVender : MonoBehaviour
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
                objective = new Objective("CatchFish", "Catch a fish for the hungry vendor: {0}/{1}", 1);
                GameManager.Instance.AddObjective(objective);
                quest = new Quest("Hungry Vendor", "The vendor is hungry, find something for him to eat.", new List<Objective>() { objective });
                GameManager.Instance.AddQuest(quest);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(quest.name);
                break;
        }
    }
}
