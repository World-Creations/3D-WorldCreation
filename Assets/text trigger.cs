using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texttrigger : MonoBehaviour
{
    public GameObject text;



    public void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
        StartCoroutine(wiater());
    }


    private IEnumerator wiater()
    {
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

}

