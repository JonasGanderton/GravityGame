using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour
{
    private PlayerProfile _player;
    private Canvas PlayerSelect;
    private Canvas LevelSelect;
    private Button[] LevelButtons;
    
    private void Awake()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        PlayerSelect = canvases[0];
        LevelSelect = canvases[1];
        PlayerSelect.enabled = true;
        LevelSelect.enabled = false;

        LevelButtons = LevelSelect.GetComponentsInChildren<Button>();
    }
    
    public void LoadPlayerData(string playerName)
    {
        TextAsset playerText = Resources.Load(playerName) as TextAsset;
        
        if (playerText == null) return; // Create new player 
        
        _player = PlayerProfile.CreateFromString(playerText.text);
    }


    public void ShowLevelSelect()
    {
        PlayerSelect.enabled = false;
        LevelSelect.enabled = true;

        // Enable unlocked levels
        for (int i = 0; i <= _player.highestLevelCompleted; i++)
        {
            LevelButtons[i].enabled = true;
        }
        
        // Disable locked levels
        for (int i = _player.highestLevelCompleted + 1; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].enabled = false;
        } 
    }
}