using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class texttrigger : MonoBehaviour
{
    public string message;
    public TMP_Text text;

    public void OnTriggerEnter(Collider other)
    {
        text.text = message;
        text.gameObject.transform.parent.gameObject.SetActive(true);
        StartCoroutine(wiater());
    }


    private IEnumerator wiater()
    {
        yield return new WaitForSeconds(4f);
        text.gameObject.transform.parent.gameObject.SetActive(false);
    }

}

