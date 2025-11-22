using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneloader : MonoBehaviour
{
    public string scene;

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(scene);
    }
}
