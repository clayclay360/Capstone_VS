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

    public GameObject[] buttonBorders;

    public Image FadeOut;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        menuCounter = 1;
        minButtons = 1;
        maxButtons = 3;
        Debug.Log(menuCounter);
    }

    // Update is called once per frame
    void Update()
    {
        SetBorders();
    }

    public void StartLevelGivenIndex(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnInteract()
    {
        if (menuCounter == 1 || menuCounter == 2)
        {
            StartLevelGivenIndex(menuCounter);
        } 
        else if (menuCounter == 3)
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

    /// <summary>
    /// Sets all borders to inactive then sets the current border to active
    /// </summary>
    private void SetBorders()
    {
        foreach (GameObject border in buttonBorders)
        {
            border.SetActive(false);
        }
        buttonBorders[menuCounter - 1].SetActive(true);
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
            } else
            {
                loadLevelBool = false;
            }
            yield return null;
        }
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
