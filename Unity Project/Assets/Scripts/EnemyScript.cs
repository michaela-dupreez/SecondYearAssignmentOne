using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 7 && collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
    //Measures the velocity of the shot.
    //If the velocity is high enough, and the Ball collides with the Enemy, the Enemy is destroyed.
}
