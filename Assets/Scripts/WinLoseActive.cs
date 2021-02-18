using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseActive : MonoBehaviour
{
    public GameObject red_win;
    public GameObject blue_win;
    public GameObject draw;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if ((Timer.isBlueWin) || (AddForce.blue_w))
        {
            blue_win.SetActive(true);
        }

        if ((Timer.isRedWin) || (AddForce.red_w))
        {
            red_win.SetActive(true);
        }

        if (Timer.isDraw)
        {
            draw.SetActive(true);
        }
    }
}
