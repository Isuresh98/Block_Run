using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingProccress : MonoBehaviour
{
    public GameObject LoaderUI;
    public Slider progressSlider;



    [SerializeField]
    private GameObject Winpannel;
    [SerializeField]
    private GameObject OverPannel;

    private void Start()
    {
        
        
    }
    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
        Winpannel.SetActive(false);
        OverPannel.SetActive(false);
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        LoaderUI.SetActive(true);
        
        

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public void RestartScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Winpannel.SetActive(false);
        OverPannel.SetActive(false);
    }

}
