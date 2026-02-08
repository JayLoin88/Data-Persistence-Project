#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;

    public string userName;
    public string highScoreUser;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI highScoreText;
    public GameObject titleScreen;
    public int highScore;
    public TextMeshProUGUI bestScore;


    private void Awake()
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindBestScoreUI();
    }

    private void FindBestScoreUI()
    {
        var byName = GameObject.Find("Best Score Text");
        if (byName != null)
        {
            bestScore = byName.GetComponent<TextMeshProUGUI>();
            if (bestScore != null) return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHighScoreText();
        BestScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else 
        Application.Quit();
#endif
    }

    public void NameEntered()
    {
        userName = nameInputField.text;
    }

    public void UpdateHighScoreIfNeeded(int score)
    {
        if (score <= highScore)
        {
            return;
        }

        highScore = score;
        highScoreUser = string.IsNullOrEmpty(userName) ? "Unknown" : userName;
        Save();
        UpdateHighScoreText();
    }

    public void UpdateHighScoreText()
    {
        if (bestScore == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(highScoreUser))
        {
            bestScore.text = "Best Score: " + highScore;
        }
        else
        {
            bestScore.text = $"Best Score: {highScoreUser} : {highScore}";
        }
    }

    public void BestScoreText()
    {
        bestScore.text = "Best Score: " + highScoreUser + highScore;
    }

    [System.Serializable]
    class SaveData
    {
        public string highScoreUser;
        public int highScore;
    }

    private void Save()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreUser = highScoreUser;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = (Application.persistentDataPath + "/savefile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data != null)
            {
                highScore = data.highScore;
                highScoreUser = data.highScoreUser;
            }
        }
    }
}
