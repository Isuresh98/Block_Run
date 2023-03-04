using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CoinCollectamount;
    public int Sheild;
    public Text coinText;
    
    public Text SheildText;

   [SerializeField]
    private GameObject Winpannel;
    [SerializeField]
    private GameObject OverPannel;


    private GameObject star1;
    private GameObject star2;
    private GameObject star3;

    // Start is called before the first frame update

    private void Awake()
    {
       

    }
    void Start()
    {
       

       

        // Display the level coin amount in the LevelcoinText

        coinText.text= CoinCollectamount.ToString();
        SheildText.text = "Sheild: " + Sheild.ToString();
        CoinCollectamount = PlayerPrefs.GetInt("Score", 0);

        star1 = GameObject.FindGameObjectWithTag("S1");
        star2 = GameObject.FindGameObjectWithTag("S2");
        star3 = GameObject.FindGameObjectWithTag("S3");


       

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

    }

    private void OnDestroy()
    {
        // Save the player's score to PlayerPrefs when the game object is destroyed
        PlayerPrefs.SetInt("Score", CoinCollectamount);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the level coin amount in the LevelcoinText
     
        coinText.text = CoinCollectamount.ToString();
        SheildText.text =  Sheild.ToString();
    }

    public void Menu(bool menu)
    {
        if (menu)
        {
            Winpannel.SetActive(true);
            
        }
        if (!menu)
        {
           
            OverPannel.SetActive(true);
        }
    }

    public void OnClearPlayerPrefsButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared.");
    }

    public void starCount(int StarCount)
    {
       
        if (StarCount == 0)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        if (StarCount == 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        if (StarCount == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        if (StarCount == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
    }
}
