using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public GameObject blackscreen;
    string scenenames;
    public TextMeshProUGUI ClassicHighScore, AirStrikeHighScore, RepelHighScore, InsaneHighScore, FlashlightHighScore;

    private void Start()
    {
        Invoke("DisplayData", .2f);
    }
    void DisplayData()
    {
        ClassicHighScore.text = Data.ClassicHighScore.ToString();
        AirStrikeHighScore.text = Data.airstrikeHighscore.ToString();
        RepelHighScore.text = Data.RepelHighscore.ToString();
        InsaneHighScore.text = Data.InsaneHighscore.ToString();
        FlashlightHighScore.text = Data.FlashlightHighScore.ToString();
    }
    public void Play(string sceneName)
    {
        blackscreen.SetActive(true);
        scenenames = sceneName;
        Invoke("switchScenes", 1.5f);
    }
    void switchScenes()
    {
        SceneManager.LoadScene(scenenames);
    }
}
