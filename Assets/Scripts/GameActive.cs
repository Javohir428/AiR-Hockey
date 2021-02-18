using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Countdown))]
public class GameActive : MonoBehaviour
{
    public GameObject startGame;


    // Update is called once per frame

    void Start()
    {
        if (Countdown.isActiveGame)
        {
            startGame.SetActive(true);
        }
    }

    private void Awake()
    {
        if (Countdown.isActiveGame)
        {
            startGame.SetActive(true);
        }
    }
    void Update()
    {
        if (Countdown.isActiveGame)
        {
            startGame.SetActive(true);
        }
    }
}
