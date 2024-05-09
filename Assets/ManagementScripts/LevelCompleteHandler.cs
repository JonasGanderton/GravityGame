using UnityEngine;

public class LevelCompleteHandler : MonoBehaviour
{
    private Canvas _levelCompleteMenu;
    private GameObject _player;

    private void Awake()
    {
        _levelCompleteMenu = GetComponent<Canvas>();
        _levelCompleteMenu.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LevelComplete()
    {
        _levelCompleteMenu.enabled = true;
        _player.SendMessage("SetLevelCompleted", true);
    }
}