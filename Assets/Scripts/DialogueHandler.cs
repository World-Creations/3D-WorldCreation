using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;




public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI spokenLine;
    [SerializeField] private Transform responseButtonPanel;
    [SerializeField] private GameObject buttonPrefab;

    private DialogueGraph dialogue;
    private Action<string> resultCallback;

    public static DialogueHandler Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    public void StartConversation(DialogueGraph dialogueRequest, Action<string> callback = null)
    {
        if (dialoguePanel.activeSelf) return; // Already in dialogue, cannot start another
        dialogue = dialogueRequest;
        resultCallback = callback;
        foreach (Node item in dialogue.nodes)
            if (item is EntryNode)
            {
                dialogue.current = item.GetPort("exit").Connection.node as CoreNodeBase;
                break;
            }

        dialoguePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        ParseNode();
    }

    private void ParseNode()
    {
        if (dialogue.current == null)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        switch (dialogue.current.GetType().Name)
        {
            case "NPCDialogueNode":
                spokenLine.text = (dialogue.current as DialogueNodeBase).dialogueSpoken;
                break;
            case "QuestionNode":
                spokenLine.text = (dialogue.current as DialogueNodeBase).dialogueSpoken;
                SpawnResponseButtons();
                break;
            case "ResponseNode":
                ClearButtons();
                NextNode("exit");
                break;
            case "ConditionalNode":
                switch (GameManager.Instance.IsQuestComplete((dialogue.current as ConditionalNode).quest))
                {
                    case true:
                        if (GameManager.Instance.IsQuestClosed((dialogue.current as ConditionalNode).quest))
                        {
                            NextNode("closed");
                        }
                        else
                        {
                            NextNode("complete");
                        }
                        break;
                    case false:
                        NextNode("incomplete");
                        break;
                    default:
                        NextNode("uninitiated");
                        break;
                }
                break;
            case "ObjectiveNode":
                ObjectiveNode objectiveNode = dialogue.current as ObjectiveNode;
                GameManager.Instance.AddProgress(objectiveNode.identifier, objectiveNode.addProgress);
                NextNode("exit");
                break;
            case "ExitNode":
                resultCallback?.Invoke((dialogue.current as ExitNode).result);
                Cursor.lockState = CursorLockMode.Locked;
                dialogue = null;
                dialoguePanel.SetActive(false);
                break;
        }
    }

    public void NextNode(string fieldName)
    {
        dialogue.current = (dialogue.current.GetPort(fieldName).Connection.node as CoreNodeBase);
        ParseNode();
    }

    private void SpawnResponseButtons()
    {
        foreach (NodePort port in dialogue.current.Outputs)
        {
            if (port.Connection == null || port.Connection.node == null)
                continue;

            ResponseNode rd = port.Connection.node as ResponseNode;

            Button b = Instantiate(buttonPrefab, responseButtonPanel).GetComponent<Button>();
            b.onClick.AddListener(() => NextNode(port.fieldName));
            b.GetComponentInChildren<TextMeshProUGUI>().text = rd.dialogueSpoken.ToString();
        }
    }

    private void ClearButtons()
    {
        Transform[] children = responseButtonPanel.GetComponentsInChildren<Transform>();

        for (int i = children.Length - 1; i > 0; i--)
            if (children[i] != responseButtonPanel)
                Destroy(children[i].gameObject);
    }

    public void MouseClick()
    {
        if (dialogue == null || dialogue.current == null || spokenLine == null || dialogue.current.GetType().Name != "NPCDialogueNode") return;

        NextNode("exit");
    }
}