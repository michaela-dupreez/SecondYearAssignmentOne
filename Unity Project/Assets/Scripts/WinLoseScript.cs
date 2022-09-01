using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Enemy") == null)
        {
            SceneManager.LoadScene("WinScreen");
        }
        //If there are no enemies left, the "WinScreen" is displayed.
        else if (GameObject.Find("EmptyBall") != null)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        //If the "EmptyBall" gameObject (i.e. an empty GameObject that I created to be loaded as the last ball)
        //is activated, and the enemies have not all been destroyed, the "LoseScreen" is displayed.

    }
}
