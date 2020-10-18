using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private void Awake()
    {
        Data.ClassicHighScore = PlayerPrefs.GetInt("ClassicHighScore");
        Data.coins = PlayerPrefs.GetInt("coins");
        Data.airstrikeHighscore = PlayerPrefs.GetInt("AirStrikeHighScore");
        Data.FlashlightHighScore = PlayerPrefs.GetInt("FlashlightHighScore");
        Data.RepelHighscore = PlayerPrefs.GetInt("RepelHighscore");
        Data.InsaneHighscore = PlayerPrefs.GetInt("InsaneHighscore");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("ClassicHighScore", Data.ClassicHighScore);
        PlayerPrefs.SetInt("AirStrikeHighScore", Data.airstrikeHighscore);
        PlayerPrefs.SetInt("RepelHighscore", Data.RepelHighscore);
        PlayerPrefs.SetInt("FlashlightHighScore", Data.FlashlightHighScore);
        PlayerPrefs.SetInt("InsaneHighscore", Data.InsaneHighscore);
        PlayerPrefs.SetInt("coins", Data.coins);
    }
}
