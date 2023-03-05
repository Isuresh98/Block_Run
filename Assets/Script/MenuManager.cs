using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{  
    [SerializeField]
    private GameObject LevelPannel;
    [SerializeField]
    private GameObject MeinMenuPannel;
    public int CoinCollectamountDisplay;
    public Text coinText;


   


    // Start is called before the first frame update
    void Start()
    {
        CoinCollectamountDisplay = PlayerPrefs.GetInt("Score", 0);

        MeinMenuPannel.SetActive(true);
        LevelPannel.SetActive(false);
        coinText.text = CoinCollectamountDisplay.ToString();

      

    }

    // Update is called once per frame
    void Update()
    {


       

    }

   
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelOn()
    {
        MeinMenuPannel.SetActive(false);
        LevelPannel.SetActive(true);
    } 
    public void MainMenuOn()
    {
        MeinMenuPannel.SetActive(true);
        LevelPannel.SetActive(false);
    }
}
