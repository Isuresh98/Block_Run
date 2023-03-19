using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int totalLevels ;
    public int lockedLevels;
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the number of locked levels
        lockedLevels = totalLevels;
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level" + nextLevelIndex.ToString(), 1);
        // Loop through the level buttons and check if the level is unlocked
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            // Check if the level is unlocked
            if (PlayerPrefs.GetInt("Level" + levelIndex.ToString(), 0) == 1)
            {
                // Unlock the level button
                levelButtons[i].interactable = true;
                lockedLevels--;
                // Change the button color to green to indicate that it's unlocked
                levelButtons[i].GetComponent<Image>().color = Color.green;
                // Enable the text of the button
                Text buttonText = levelButtons[i].GetComponentInChildren<Text>();
                buttonText.enabled = true;
            }
            else
            {
                // Lock the level button
                levelButtons[i].interactable = false;
                // Change the button color to red to indicate that it's locked
                levelButtons[i].GetComponent<Image>().color = Color.red;
                // Disable the text of the button
                Text buttonText = levelButtons[i].GetComponentInChildren<Text>();
                buttonText.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenScene(int level)
    {
        // Check if the level is unlocked
        if (PlayerPrefs.GetInt("Level" + level.ToString(), 0) == 1)
        {
            // Load the scene
            SceneManager.LoadScene(level);
        }
    }

    public void FirstOpenScence()
    {
        // Loop through the level indices in reverse order and find the index of the last unlocked level
        for (int i = totalLevels; i >= 1; i--)
        {
            if (PlayerPrefs.GetInt("Level" + i.ToString(), 0) == 1)
            {
                // Set the last unlocked level as the active scene
                SceneManager.LoadScene(i);
                break;
            }
        }

    }
}
