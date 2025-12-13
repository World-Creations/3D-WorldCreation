using System.Collections.Generic;
using UnityEngine;

public class AJobToDo : MonoBehaviour
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
                objective = new Objective("CatchFish", "Catch fish for the job: {0}/{1}", 3);
                GameManager.Instance.AddObjective(objective);
                quest = new Quest("A Job To Do", "You've got work to do, get it done.", new List<Objective>() { objective });
                GameManager.Instance.AddQuest(quest);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(quest.name);
                break;
        }
    }
}
