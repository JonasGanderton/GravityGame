public interface AchievementInterface
{
    bool Unlock(string title);
    bool IsUnlocked(string title);
    bool CreateNewAchievement(string title, bool unlocked);
}