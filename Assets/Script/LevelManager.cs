using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int totalLevels = 3;
    public int lockedLevels;
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the number of locked levels
        lockedLevels = totalLevels;

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
            }
            else
            {
                // Lock the level button
                levelButtons[i].interactable = false;
                // Change the button color to red to indicate that it's locked
                levelButtons[i].GetComponent<Image>().color = Color.red;
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
}
