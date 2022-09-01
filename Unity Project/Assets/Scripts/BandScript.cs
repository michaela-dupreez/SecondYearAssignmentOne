using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandScript : MonoBehaviour
{
    public static int BandVisible = 0;

    // Update is called once per frame
    void Update()
    {
        if(BandScript.BandVisible == 1)
        {
            GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            GetComponent<LineRenderer>().enabled = true;
        }
        //Set up for band visibility.
        //Used to make the band disappear after the ball has been launched, so a new band will appear when the next ball 
        //is added to the catapault.
    }
}
