using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    [SerializeField] public string playerName;
    [SerializeField] public int highestLevelCompleted;

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
        // Could use Application.persistentDataPath if needed? 
        File.WriteAllText("Assets/Resources/" + playerName + ".json", ConvertToString());
    }
}