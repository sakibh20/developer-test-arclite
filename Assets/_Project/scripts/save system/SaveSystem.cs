using UnityEngine;
using System;

public static class SaveSystem
{
    private const string KeyPrefix = "GAME_SAVE_";
    
    public static void Save<T>(string key, T value)
    {
        string fullKey = GetFullKey(key);

        if (value == null)
        {
            Debug.LogWarning($"SaveSystem: Trying to save null value for key {key}");
            return;
        }

        string json = JsonUtility.ToJson(new Wrapper<T>(value));
        PlayerPrefs.SetString(fullKey, json);
        PlayerPrefs.Save();
    }
    
    public static T Load<T>(string key, T defaultValue = default)
    {
        string fullKey = GetFullKey(key);

        if (!PlayerPrefs.HasKey(fullKey))
            return defaultValue;

        string json = PlayerPrefs.GetString(fullKey);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Value;
    }
    
    public static bool Exists(string key)
    {
        return PlayerPrefs.HasKey(GetFullKey(key));
    }
    
    public static void Delete(string key)
    {
        string fullKey = GetFullKey(key);
        if (PlayerPrefs.HasKey(fullKey))
        {
            PlayerPrefs.DeleteKey(fullKey);
        }
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private static string GetFullKey(string key)
    {
        return $"{KeyPrefix}{key}";
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T Value;
        public Wrapper(T value) => Value = value;
    }
}