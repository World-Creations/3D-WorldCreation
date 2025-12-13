using System.Collections.Generic;
using UnityEngine;

public class ALostSock : MonoBehaviour
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
                objective = new Objective("CatchSockrates", "Catch a Sockrates: {0}/{1}", 1);
                GameManager.Instance.AddObjective(objective);
                quest = new Quest("A Lost Sock", "No one likes wet socks.", new List<Objective>() { objective });
                GameManager.Instance.AddQuest(quest);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(quest.name);
                break;
        }
    }
}
