using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneloader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private string target;

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.name);
        if (target == "" || target == null) target = "Default";
        if (other.tag == "Player") other.GetComponent<PlayerController>().ChangeScene(scene, target);
    }
}
