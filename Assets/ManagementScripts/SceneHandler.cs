using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void SetScene(string scene)
    {
        PlayerPrefs.SetString("CurrentScene", scene);
        SceneManager.LoadScene(scene);
    }
}