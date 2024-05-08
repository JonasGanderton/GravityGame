using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void SetScene(string scene)
    {
        Debug.Log("Loading scene: " + scene);
        SceneManager.LoadScene(scene);
    }
}