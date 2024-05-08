using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void SetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}