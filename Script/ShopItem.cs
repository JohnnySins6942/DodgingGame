using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header ("data")]
    public Sprite image;
    public string Name;
    public int cost;
    public bool isBrought;
    public bool isEquipped;
    public string saveKey1;
    public string savekey2;
    public Material skin;

    [Header("Components")]
    public TextMeshProUGUI Title;
    public TextMeshProUGUI ButtonText;
    public GameObject Button;
    public Image Display;
    public Color color;
    public bool isBasic;

    private void Awake()
    {
        isBrought = (PlayerPrefs.GetInt(saveKey1) != 0);
        isEquipped = (PlayerPrefs.GetInt(savekey2) != 0);
    }

    public void Start()
    {
        DisplayUI();
    }

    public void DisplayUI()
    {
        Title.text = Name;
        if (!isBrought && !isEquipped)
        {
            ButtonText.text = cost.ToString();
        }
        else if(isBrought && !isEquipped)
        {
            ButtonText.text = "Equip";
            Button.GetComponent<Image>().color = new Color(0.282143f, 0.4078431f, 0.2745098f, 1);
        }
        else if (isBrought && isEquipped)
        {
            ButtonText.text = "Equipped!";
        }
        Display.sprite = image;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(saveKey1, (isBrought ? 1 : 0));
        PlayerPrefs.SetInt(savekey2, (isEquipped ? 1 : 0));
    }
}
