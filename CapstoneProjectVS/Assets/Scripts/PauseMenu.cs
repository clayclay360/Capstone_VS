using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Controls")]
    public int pauseMenuCounter;
    public int maxButtons;
    public int minButtons;

    public GameObject[] buttonBorders;

    public GameObject pauseMenuUI;
    public GameObject pauseMenuUI2;

    public Image FadeOut;
    public Color color;

    void Start()
    {
        //Controls
        pauseMenuCounter = 1;
        minButtons = 1;
        maxButtons = 3;
        Debug.Log(pauseMenuCounter);

    }

    

    // Update is called once per frame
    void Update()
    {
        //if (gamepad.startButton.isPressed)
        //{
        //    Paused();
        //}   
        SetBorders();
    }

    public void StartLevelGivenIndex(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void OnInteract()
    {
        if (pauseMenuCounter == 1 || pauseMenuCounter == 2)
        {
            StartLevelGivenIndex(pauseMenuCounter);
        }
        else if (pauseMenuCounter == 3)
        {
            QuitGame();
        }
    }


    #region Controls
    ////Check Controller input for the button selection
    public void OnNextButton()
    {
        if (pauseMenuCounter < maxButtons)
        {
            pauseMenuCounter += 1;
            Debug.Log(pauseMenuCounter);
        }
    }

    public void OnPreviousButton()
    {
        if (pauseMenuCounter > minButtons)
        {
            pauseMenuCounter -= 1;
            Debug.Log(pauseMenuCounter);
        }
    }

    private void SetBorders()
    {
        foreach (GameObject border in buttonBorders)
        {
            border.SetActive(false);
        }
        buttonBorders[pauseMenuCounter - 1].SetActive(true);
    }
    #endregion


    public void Paused()
    {
            Time.timeScale = 0;
            GameManager.gameIsPaused = true;
            //CheckControls();
            pauseMenuUI.SetActive(true);
            pauseMenuUI2.SetActive(true);

    }


    public void Resume()
    {
        
            Time.timeScale = 1;
            GameManager.gameIsPaused = false;
            pauseMenuUI.SetActive(false);
            pauseMenuUI2.SetActive(false);

    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        bool loadLevelBool = true;
        int subtract = 0;

        while (loadLevelBool)
        {
            if (subtract < 300)
            {
                color.a += 0.005f;
                FadeOut.color = color;
                subtract += 1;
            }
            else
            {
                loadLevelBool = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Debug.Log("Qutting game...");
        Application.Quit();
    }
}
