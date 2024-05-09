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
        LoadPlayerData(PlayerPrefs.GetString("CurrentPlayer"));
        
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        if (canvases[0].CompareTag("LevelCompleteMenu")) return;
        
        // Only on main menu scene
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
        PlayerPrefs.SetString("CurrentPlayer", _player.playerName);
        PlayerPrefs.Save();
    }


    public void ShowLevelSelect()
    {
        PlayerSelect.enabled = false;
        LevelSelect.enabled = true;

        // Enable unlocked levels
        for (int i = 0; i <= _player.highestLevelCompleted && i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = true;
        }
        
        // Disable locked levels
        for (int i = _player.highestLevelCompleted + 1; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        } 
    }

    public void LevelCompleted()
    {
        string scene = PlayerPrefs.GetString("CurrentScene");
        int level = int.Parse(scene.Substring(scene.Length - 2));
        if (_player.highestLevelCompleted < level)
        {
            _player.highestLevelCompleted = level;
            _player.SaveToFile();
        }
    }
}