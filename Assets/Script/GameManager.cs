using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private GameObject star1_2;
    [SerializeField]
    private GameObject star2_2;
    [SerializeField]
    private GameObject star3_2;

    //audio Effecr
    private AudioSource audioSource;

    public Text sceneIndexText;
    public Text sceneIndexText2;

    // Start is called before the first frame update

    private void Awake()
    {
       

    }
    void Start()
    {
        //Audio 
        // Get the current scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Display the scene index in the UI Text
        sceneIndexText.text = "Level  " + sceneIndex.ToString();
        sceneIndexText2.text = "Level  " + sceneIndex.ToString();

        star1 = GameObject.FindGameObjectWithTag("S1");
        star2 = GameObject.FindGameObjectWithTag("S2");
        star3 = GameObject.FindGameObjectWithTag("S3");

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        star1_2.SetActive(false);
        star2_2.SetActive(false);
        star3_2.SetActive(false);

      
        // Display the level coin amount in the LevelcoinText

        coinText.text= CoinCollectamount.ToString();
        SheildText.text = "Sheild: " + Sheild.ToString();
        CoinCollectamount = PlayerPrefs.GetInt("Score", 0);

        

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

            star1_2.SetActive(false);
            star2_2.SetActive(false);
            star3_2.SetActive(false);
        }
        if (StarCount == 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);

            star1_2.SetActive(true);
            star2_2.SetActive(false);
            star3_2.SetActive(false);
        }
        if (StarCount == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);

            star1_2.SetActive(true);
            star2_2.SetActive(true);
            star3_2.SetActive(false);
        }
        if (StarCount == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);

            star1_2.SetActive(true);
            star2_2.SetActive(true);
            star3_2.SetActive(true);
        }
    }
}
