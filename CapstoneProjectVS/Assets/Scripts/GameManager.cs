using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static bool gameStarted;
    public static bool assigningOrders;
    public static bool tutorialLevel;
    public static bool gameIsPaused;
    public static int currentLevel;
    public static float rating;

    public static bool isTouchingTrashCan;
    public static bool passItemsReady;
    public static string[] counterItems = { "", "", ""};
    public static bool putOnCounter;


    public bool reqsClear;

    public static int numberOfPlayers = 0;
    public static bool cookBookActive = true;

    public static int ratCount = 0;
    public static int numberOfRatsKilled = 0;

    public int playerScore;
    public int scoreMultiplier;

    //public static PlayerController playerOne;
    //public static PlayerController playerTwo;
}
