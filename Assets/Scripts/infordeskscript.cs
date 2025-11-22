using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class infordeskscript : MonoBehaviour
{
    public GameObject text;



    public void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
        StartCoroutine(waiter());
    }


    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
