using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    private bool isMuted;
    public Toggle myToggle;
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
        
    }

    private void Awake() 
    {
        if (PlayerPrefs.GetInt("MUTED") == 1)
        {
            myToggle.isOn = true;
        } 
        if (PlayerPrefs.GetInt("MUTED") == 0)
        {
            myToggle.isOn = false;
        } 
        
    }
    
    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }
}
