using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private GameObject LevelPannel;
    private GameObject MeinMenuPannel;
    // Start is called before the first frame update
    void Start()
    {
        MeinMenuPannel = GameObject.FindGameObjectWithTag("MainMenuPanel");
        LevelPannel = GameObject.FindGameObjectWithTag("LevelPanel");

        MeinMenuPannel.SetActive(true);
        LevelPannel.SetActive(false);
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
