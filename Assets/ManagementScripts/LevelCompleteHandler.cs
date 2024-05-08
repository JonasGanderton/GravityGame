using UnityEngine;

public class LevelCompleteHandler : MonoBehaviour
{
    private Canvas _levelCompleteMenu;

    private void Awake()
    {
        _levelCompleteMenu = GetComponent<Canvas>();
        _levelCompleteMenu.enabled = false;
    }

    public void LevelComplete()
    {
        _levelCompleteMenu.enabled = true;
    }
}