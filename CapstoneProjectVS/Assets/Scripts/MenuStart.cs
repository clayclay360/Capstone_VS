using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public int menuCounter;
    public int maxButtons;
    public int minButtons;

    public GameObject StartGameBorder;
    public GameObject QuitGameBorder;

    public Image FadeOut;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        menuCounter = 1;
        minButtons = 1;
        maxButtons = 2;
        Debug.Log(menuCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuCounter == 1)
        { SGBorderActive(); }
        else if (menuCounter == 2)
        { QGBorderActive();  }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        StartCoroutine(LoadFirstLevel());
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OnInteract()
    {
        if (menuCounter == 1)
        {
            StartGame();
        } else if (menuCounter == 2)
        {
            QuitGame();
        }
    }

    public void OnNextRecipe()
    {
        if (menuCounter < maxButtons)
        {
            menuCounter += 1;
            Debug.Log(menuCounter);
        }
    }

    public void OnPreviousRecipe()
    {
        if (menuCounter > minButtons)
        {
            menuCounter -= 1;
            Debug.Log(menuCounter);
        }
    }

    public void SGBorderActive()
    {
        StartGameBorder.SetActive(true);
        QuitGameBorder.SetActive(false);
    }

    public void QGBorderActive()
    {
        StartGameBorder.SetActive(false);
        QuitGameBorder.SetActive(true);
    }

    private IEnumerator LoadFirstLevel()
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
            } else
            {
                loadLevelBool = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
