#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{

    public string userName;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI highScoreText;
    public float highScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText.text = "High Score: " + highScore;
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
        if (nameInputField == null)
        {
            Debug.LogWarning("Name input Field is not assigned.");
            return;
        }

        userName = nameInputField.text;

        string json = JsonUtility.ToJson(userName);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}
