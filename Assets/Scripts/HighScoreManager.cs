using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{

    public static HighScoreManager Instance { get; private set; }

    public string HighScoreUser { get; private set; } = "";
    public int HighScore { get; private set; } = 0;

    public string CurrentPlayerName { get; set; } = "";


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public bool TrySetHighScore(int score)
    {
        if (score <= HighScore) return false;

        HighScore = score;
        HighScoreUser = string.IsNullOrEmpty(CurrentPlayerName) ? "Unknown" : CurrentPlayerName;
        Save();
        return true;
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScoreUser;
        public int HighScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.HighScoreUser = HighScoreUser;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScoreUser = data.HighScoreUser;
            HighScore = data.HighScore;
        }
    }
}
