using UnityEngine;

public class AchievementService : MonoBehaviour
{
    private AchievementInterface abstractAchievements;
    private void Awake()
    {
        abstractAchievements = GetComponent<AchievementInterface>();
    }

    public bool Unlock(string title)
    {
        if (!abstractAchievements.Unlock(title))
        {
            return CreateNewAchievement(title, true);
        }

        return true;
    }

    public bool IsUnlocked(string title)
    {
        return abstractAchievements.IsUnlocked(title);
    }

    public bool CreateNewAchievement(string title, bool unlocked = false)
    {
        Debug.Log("Creating achievement: " + title + " " + unlocked);
        return abstractAchievements.CreateNewAchievement(title, unlocked);
    }
}