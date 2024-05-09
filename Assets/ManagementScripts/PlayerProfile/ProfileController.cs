using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour
{
    private PlayerProfile _player;
    private AchievementService _achievementService;
    private Canvas PlayerSelect;
    private Canvas LevelSelect;
    private Button[] LevelButtons;
    
    private void Awake()
    {
        EnsureFilesExist();
        LoadPlayerData(PlayerPrefs.GetString("CurrentPlayer"), false);
        _achievementService = GetComponent<AchievementService>();
        
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
        LoadPlayerData(playerName, true);
    }
    private void LoadPlayerData(string playerName, bool displayLevelSelect)
    {
        string playerText = File.ReadAllText(Application.persistentDataPath + "/" + playerName + ".json");
        _player = PlayerProfile.CreateFromString(playerText);

        PlayerPrefs.SetString("CurrentPlayer", _player.playerName);
        PlayerPrefs.Save();

        if (displayLevelSelect) ShowLevelSelect();
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
        _achievementService.Unlock("Completed level " + level + "!");
    }

    public PlayerProfile GetPlayerProfile()
    {
        return _player;
    }

    private void EnsureFilesExist()
    {
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Application.persistentDataPath + "/P" + i + ".json";
            if (File.Exists(filePath)) continue;
            
            _player = new PlayerProfile();
            _player.playerName = "P" + i;
            _player.SaveToFile();
        }

    }
}