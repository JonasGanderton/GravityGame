using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    [SerializeField] public string playerName;
    [SerializeField] public int highestLevelCompleted;

    public string ConvertToString()
    {
        return JsonUtility.ToJson(this);
    }

    public static PlayerProfile CreateFromString(string jsonString)
    {
        return JsonUtility.FromJson<PlayerProfile>(jsonString);
    }
}