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
    [SerializeField]
    private GameObject OptionPannel;
    public int CoinCollectamountDisplay;
    public Text coinText;


    //sound
    public AudioClip BGSound;
    private AudioSource audioSource;
    [SerializeField]
    private Slider volumeSlider;




    // Start is called before the first frame update
    void Start()
    {
        //sound effect
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGSound;
        audioSource.loop = true;
        audioSource.Play();
        volumeSlider.value = audioSource.volume;


        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        CoinCollectamountDisplay = PlayerPrefs.GetInt("Score", 0);

        MeinMenuPannel.SetActive(true);
        LevelPannel.SetActive(false);
        OptionPannel.SetActive(false);
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
        OptionPannel.SetActive(false);
    } 
    public void MainMenuOn()
    {
        MeinMenuPannel.SetActive(true);
        LevelPannel.SetActive(false);
        OptionPannel.SetActive(false);
    }

    public void OptionOn()
    {
        MeinMenuPannel.SetActive(false);
        LevelPannel.SetActive(false);
        OptionPannel.SetActive(true);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
