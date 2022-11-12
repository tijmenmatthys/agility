using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    void Start()
    {
        _scoreText.text = ScoreText();
    }

    private static string ScoreText()
    {
        int level = GlobalPlayerData.LastScore;
        if (level < 0) return "";

        string line1 = $"You Survived {level} levels.";

        string line2;
        if (level < 5) line2 = "That's a bit pathetic...";
        else if (level < 10) line2 = "Well, it's better than nothing.";
        else if (level < 15) line2 = "Alright, now we're talking!";
        else if (level < 20) line2 = "GG, gamer!";
        else line2 = "You're on fire!";

        return line1 + "\n" + line2;
    }

    public static void LaunchGame()
    {
        SceneManager.LoadScene("Game");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void MoreGames()
    {
        Application.OpenURL("https://tijmenmatthys.itch.io/");
    }
}
