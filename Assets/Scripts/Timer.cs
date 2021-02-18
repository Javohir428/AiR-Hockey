using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshPro textDisplay1;
    public TextMeshPro textDisplay2;
    public int secondsLeft = 20;


    public GameObject puck;

    public static bool isRedWin = false;
    public static bool isBlueWin = false;
    public static bool isDraw = false;

    public GameObject startGame;

    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay1.text = "" + secondsLeft;
        textDisplay2.text = "" + secondsLeft;
        StartCoroutine(CountdownTimer());

    }

    IEnumerator CountdownTimer()
    {
        while(secondsLeft > 0)
        {
            textDisplay1.text = "" + secondsLeft;
            textDisplay2.text = "" + secondsLeft;

            yield return new WaitForSeconds(1f);

            secondsLeft--;
        }

        textDisplay1.text = "0";
        textDisplay2.text = "0";

        if ((secondsLeft == 0) && (puck.GetComponent<AddForce>().blue_score > puck.GetComponent<AddForce>().red_score))
        {
            winSound.Play();
            startGame.SetActive(false);
            isBlueWin = true;
        }
        if ((secondsLeft == 0) && (puck.GetComponent<AddForce>().blue_score < puck.GetComponent<AddForce>().red_score))
        {
            winSound.Play();
            startGame.SetActive(false);
            isRedWin = true;
        }
        if ((secondsLeft == 0) && (puck.GetComponent<AddForce>().blue_score == puck.GetComponent<AddForce>().red_score))
        {
            winSound.Play();
            startGame.SetActive(false);
            isDraw = true;
        }

        yield return new WaitForSeconds(1f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
