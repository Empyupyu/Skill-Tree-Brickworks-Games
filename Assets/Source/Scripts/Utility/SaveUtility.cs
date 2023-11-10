using UnityEngine;

public class SaveUtility
{
    private static SaveUtility _instance;

    public static SaveUtility Instance()
    {
        if (_instance == null)
        {
            _instance = new SaveUtility();
        }

        return _instance;
    }

    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public string Load(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public bool HasSave(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
