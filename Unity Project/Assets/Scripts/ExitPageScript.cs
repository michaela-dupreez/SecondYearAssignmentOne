using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPageScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.Quit();
            }
        }
        //Exits the game when the 'Exit' button is pressed with the left mouse button
    }
}
