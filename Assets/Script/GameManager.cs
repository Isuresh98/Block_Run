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
    public GameObject MenuUI;
    public GameObject MenuFUI;
    // Start is called before the first frame update
    void Start()
    {
        MenuUI.SetActive(false);
        MenuFUI.SetActive(false);
        coinText.text = "Coin Ammount: " + CoinCollectamount.ToString();
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
        coinText.text =   CoinCollectamount.ToString();
        SheildText.text =   Sheild.ToString();
    }
    public void Menu(bool menu)
    {
        if (menu)
        {
            MenuUI.SetActive(true);

        }
        if (!menu )
        {
            MenuFUI.SetActive(true);
        }
  
    }
    public void OnClearPlayerPrefsButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared.");
    }
}
