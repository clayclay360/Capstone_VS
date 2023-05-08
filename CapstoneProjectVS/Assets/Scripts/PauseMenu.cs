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

    public Image FadeOut;
    public Color color;


    public GameObject pauseMenuUI;
    public GameObject pauseMenuUI2;

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
    //Check Controller input for the button selection
    //private void CheckControls()
    //{
    //    if (gamepad == null) { return; }
    //    //Close the controls menu
    //    if (controlMenuIsUp)
    //    {
    //        if (gamepad.buttonWest.wasPressedThisFrame)
    //        {
    //            howToPlayQuitButt.onClick.Invoke();
    //            controlMenuIsUp = false;
    //        }
    //        return;
    //    }


    //    //Navigate up or down depending on controller input
    //    //Gets y value of left controller stick
    //    float stickL = gamepad.leftStick.ReadValue().y;
    //    if (stickL > .25f && !axisLocked)
    //    {
    //        //Increase the number or loop back to 0
    //        selectedButt = selectedButt == 0 ? PASBUTTNUM - 1 : selectedButt - 1;
    //        axisLocked = true;
    //    }
    //    else if (stickL < -.25f && !axisLocked)
    //    {
    //        //Decrease the number or loop back to max
    //        selectedButt = selectedButt == PASBUTTNUM - 1 ? 0 : selectedButt + 1;
    //        axisLocked = true;
    //    }
    //    else if (axisLocked && stickL > -.25f && stickL < .25f)
    //    {
    //        axisLocked = false;
    //    }




    //    //Evoke the selected button if the controller button was pressed
    //    if (gamepad.buttonWest.wasPressedThisFrame)
    //    {
    //        Button[] buttons = { menuButt, howToPlayButt, quitButt, resumeButt };
    //        buttons[selectedButt].onClick.Invoke();
    //        if (buttons[selectedButt] == howToPlayButt) //Sets state for controls menu
    //        {
    //            controlMenuIsUp = true;
    //        }
    //    }


    //}

    public void OnNextRecipe()
    {
        if (pauseMenuCounter < maxButtons)
        {
            pauseMenuCounter += 1;
            Debug.Log(pauseMenuCounter);
        }
    }

    public void OnPreviousRecipe()
    {
        if (pauseMenuCounter > minButtons)
        {
            pauseMenuCounter -= 1;
            Debug.Log(pauseMenuCounter);
        }
    }

    /// <summary>
    /// Sets all borders to inactive then sets the current border to active
    /// </summary>
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


    //public void LoadMenu()
    //{
    //    Debug.Log("Loading Menu...");
    //    SceneManager.LoadScene("MainMenu");
    //}


    public void QuitGame()
    {
        Debug.Log("Qutting game...");
        Application.Quit();
    }
}
