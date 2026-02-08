using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (highScoreText == null) return;

        var mgr = HighScoreManager.Instance;
        if (mgr == null)
        {
            highScoreText.text = $"Best Score: --";
            return;
        }

        if (string.IsNullOrEmpty(mgr.HighScoreUser))
            highScoreText.text = $"Best Score: {mgr.HighScore}";
        else
            highScoreText.text = $"Best Score: {mgr.HighScore} ({mgr.HighScoreUser})";
    }
}
