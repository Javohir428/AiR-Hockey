using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reanchor : MonoBehaviour
{
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
        Countdown.isActiveGame = false;
        Timer.isBlueWin = false;
        Timer.isRedWin = false;
        Timer.isDraw = false;
        AddForce.blue_w = false;
        AddForce.red_w = false;
    }

}
