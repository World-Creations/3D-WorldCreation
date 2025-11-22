using UnityEngine;

public class ConversationDing : MonoBehaviour
{
    [SerializeField] private AudioSource asource;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            asource.Play();
        }
    }
}
