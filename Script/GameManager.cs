using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Classic,
    airstirke,
    Repel, 
    Flashlight,
    Insane
}

public class GameManager : MonoBehaviour
{
    public int Points;
    public TextMeshProUGUI PointsText;
    private MovementScript player;
    public bool IsTimeSlow;
    public GameObject DeathScreen;
    public GameObject blackScreen;
    Scene m_Scene;
    string sceneName;
    public TextMeshProUGUI coinText;
    private bool hasUpdatedCoins;
    public int Coins;
    public GameMode mode;
    private SaveAndLoad save;
    public float TimeSlowTime = 12f;
    private bool canTimeSlow = true;
    public GameObject TimeSlowScreen;
    private void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        player = FindObjectOfType<MovementScript>();
        coinText.text = Coins.ToString();
        save = FindObjectOfType<SaveAndLoad>();
        coinText.text = Data.coins.ToString();
    }

    private void Update()
    { 
        PointsText.text = Points.ToString();
    }
    private void LateUpdate()
    {
        if (ShopData.selectedItem != null)
        {
            print(ShopData.selectedItem.Name);
            if (ShopData.selectedItem.isBasic)
            {
                print(ShopData.selectedItem.Name);
                player.GetComponent<SpriteRenderer>().color = ShopData.selectedItem.color;
            }
        }
        if (player.isDead)
        {
            DeathScreen.SetActive(true);
            switch (mode)
            {
                case GameMode.Classic:
                    if (Points >= Data.ClassicHighScore)
                    {
                        Data.ClassicHighScore = Points;
                    }
                    break;
                case GameMode.airstirke:
                    if (Points >= Data.airstrikeHighscore)
                    {
                        Data.airstrikeHighscore = Points;
                    }
                    break;
                case GameMode.Repel:
                    if (Points >= Data.RepelHighscore)
                    {
                        Data.RepelHighscore = Points;
                    }
                    break;
                case GameMode.Flashlight:
                    if(Points>= Data.FlashlightHighScore)
                    {
                        Data.FlashlightHighScore = Points;
                    }
                    break;
                case GameMode.Insane:
                    if (Points >= Data.InsaneHighscore)
                    {
                        Data.InsaneHighscore = Points;
                    }
                    break;
            }
            if (!hasUpdatedCoins)
            {
                Data.coins += Coins;
                hasUpdatedCoins = true;
            }
        }
    }
    public void GetCoins(int amount)
    {
        Coins += amount;
        coinText.text = (Coins + Data.coins).ToString();
    }

    public void UseTimeSlow()
    {
        StartCoroutine(TimeSlow());
    }

    IEnumerator TimeSlow()
    {
        if (canTimeSlow)
        {
            canTimeSlow = false;
            IsTimeSlow = true;
            Time.timeScale /= 2;
            TimeSlowScreen.SetActive(true);
            yield return new WaitForSeconds(TimeSlowTime-1f);
            var anim = TimeSlowScreen.GetComponent<Animator>();
            anim.SetBool("Fading", true);
            yield return new WaitForSeconds(1f);
            TimeSlowScreen.SetActive(false);
            Time.timeScale = Mathf.Log(Points) / 2;
            canTimeSlow = true;

        }
    }

    public void PressPlay()
    {
        blackScreen.SetActive(true);
        save.Save();
        Invoke("Restart", 1.5f);
    }
    public void AddPoints(int POINTS)
    {
        Points += POINTS;
        if (Mathf.Log(Points) / 2 >= 1 && IsTimeSlow == false && mode == GameMode.Classic)
        {
            Time.timeScale = Mathf.Log(Points)/2;
        }
        else if (Mathf.Log(Points) / 4 >= .5f && IsTimeSlow == false && mode == GameMode.airstirke)
        {
            Time.timeScale = Mathf.Log(Points) / 4;
        }
        else if (Mathf.Log(Points) >= 1 && IsTimeSlow == false && mode == GameMode.Insane)
        {
            Time.timeScale = Mathf.Log(Points);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
    public void GoBackHome()
    {
        blackScreen.SetActive(true);
        Time.timeScale = 1;
        save.Save();
        Invoke("HOME", 1.5f);
    }
    void HOME()
    {
        SceneManager.LoadScene("Home");
    }
    private void OnApplicationQuit()
    {
        Data.coins += Coins;
        save.Save();
    }
}
