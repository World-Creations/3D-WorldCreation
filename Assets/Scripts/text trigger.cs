using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//
public class texttrigger : MonoBehaviour
{
    public string message;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        other.GetComponent<PlayerController>().DisplayMessage(message);
    }

}

