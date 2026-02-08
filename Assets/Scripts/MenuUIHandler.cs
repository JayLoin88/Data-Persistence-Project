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


    public TMP_InputField nameInputField;
    public TextMeshProUGUI menuHighScoreText;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      if (HighScoreManager.Instance != null && menuHighScoreText != null)
        {
            var mgr = HighScoreManager.Instance;
            if (string.IsNullOrEmpty(mgr.HighScoreUser))
                menuHighScoreText.text = $"Best: {mgr.HighScore}";
            else
                menuHighScoreText.text = $"Best: {mgr.HighScore} ({mgr.HighScoreUser})";
        }
    }

    public void OnNameEntered (string value)
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.CurrentPlayerName = value ?? "";
        }
    }

    public void StartGame()
    {
        if (nameInputField != null && HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.CurrentPlayerName = nameInputField.text ?? "";
        }

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

}
