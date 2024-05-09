using UnityEngine;

[System.Serializable]
public class Achievement
{
    [SerializeField] public string title;
    [SerializeField] public bool unlocked;

    public string ConvertToString()
    {
        return JsonUtility.ToJson(this, true);
    }

    public static Achievement CreateFromString(string jsonString)
    {
        return JsonUtility.FromJson<Achievement>(jsonString);
    }
}