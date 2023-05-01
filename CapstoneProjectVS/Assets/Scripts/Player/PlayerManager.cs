using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public GameObject numberOfPlayersNeededUI;

    public void OnPlayerJoined()
    {
        GameManager.numberOfPlayers++;

        if(GameManager.numberOfPlayers == 2)
        {
            GameManager.gameStarted = true;
            numberOfPlayersNeededUI.SetActive(false);
        }
    }
}
