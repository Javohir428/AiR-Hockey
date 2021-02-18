using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countdownTextField;

    public static bool isActiveGame = false;


    public GameObject countdownText;


    public void StartCountdown()
    {
        countdownText.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        countdownTextField.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "Go!";
        isActiveGame = true;
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "";
        countdownText.SetActive(false);
        yield return null;
    }
}
