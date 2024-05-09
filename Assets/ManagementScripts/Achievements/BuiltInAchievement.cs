using UnityEngine;

public class BuiltInAchievements : MonoBehaviour, AchievementInterface
{
    private PlayerProfile _player;

    private void Start()
    {
        _player = GetComponent<ProfileController>().GetPlayerProfile();
    }

    public bool Unlock(string title)
    {
        foreach (Achievement a in _player.achievements)
        {
            if (a.title == title)
            {
                a.unlocked = true;
                _player.SaveToFile();
                return true;
            }
        }

        return false;
    }

    public bool IsUnlocked(string title)
    {
        foreach (Achievement a in _player.achievements)
        {
            if (a.title == title) return a.unlocked;
        }

        return false;
    }

    public bool CreateNewAchievement(string newTitle, bool newUnlocked)
    {
        Achievement newAchievement = new Achievement
        {
            title = newTitle,
            unlocked = newUnlocked
        };
        _player.achievements.Add(newAchievement);
        _player.SaveToFile();
        return true;
    }
}