using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private void Update()
    {

        snail.ManageGame.PauseGame(true);
    }

    public void StartGame()
    {
        snail.ManageGame.PauseGame(false);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
