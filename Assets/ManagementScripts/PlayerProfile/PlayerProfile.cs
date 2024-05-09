using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    [SerializeField] public string playerName;
    [SerializeField] public int highestLevelCompleted;
    [SerializeField] public List<Achievement> achievements;

    public string ConvertToString()
    {
        return JsonUtility.ToJson(this, true);
    }

    public static PlayerProfile CreateFromString(string jsonString)
    {
        return JsonUtility.FromJson<PlayerProfile>(jsonString);
    }

    public void SaveToFile()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + playerName + ".json", ConvertToString());
    }
}