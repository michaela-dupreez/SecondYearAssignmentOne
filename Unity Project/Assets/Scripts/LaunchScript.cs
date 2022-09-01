using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D Hook;
    //Applying the physics engine to the rb and Hook.

    public float maxDragDistance = 3f;
    public float releaseTime = 0.2f;
    //Creating floats for the Maximum Drag Distance of the projectile and the time it takes for the ball to be released from the catapault.

    public GameObject NextBall;
    //Creating a GameObject for the Ball to be replaced after it has been shot.

    private bool isPressed = false;
    //Boolean to apply to whether or not the mouse buttons are being clicked.

    public LineRenderer catapultLineBack;
    public LineRenderer catapultLineFront;
    //The points where lines will be rendered from, to create the band of the catapault. 

    private Ray leftCatapultToProjectile;
    //The ray that will be cast from the catapault to the target.

    private float Radius;

    // Start is called before the first frame update.
    void Start()
    {
        LineRendererSetup();
        //Setting up the band of the catapault. 
        Radius = 0.6f;
        //Size of the band. 
        leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        //Setting up the (position, distance and magnitude of the) ray that will be created when the ball is launched.
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, Hook.position) > maxDragDistance)
                rb.position = Hook.position + (mousePos - Hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos; 
        }
        //Connects the Hook to the Mouse to drag the ball

        LineRendererUpdate();
        //Creates the band continuously (through update).

    }

    void LineRendererSetup()
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
        //The world space position of the band of the catapault.  

        catapultLineFront.sortingLayerName = "Foreground";
        catapultLineBack.sortingLayerName = "Background";
        //Names of the layers for the front and back of the scene. 

        catapultLineFront.sortingOrder = 3;
        catapultLineBack.sortingOrder = 1;
        //The order in which the layers will appear (The front of the catapault will be in front of the back of the catapault).
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
        //The horizontal and vertical positions from the catapault to the ball once it has been released.
        leftCatapultToProjectile.direction = catapultToProjectile;
        //The direction of the ray from the catapault to the Ball.
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + Radius);
        //Finding where the band is pulled to, to determine the length and distance of where the ball will be sent. 
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
        //The meeting point of where the two bands meet when the ball is pulled back. 
    }

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
        //The rigidbody (of the GameObject) is kinetic when the mouse button is down. 
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        //The rigidbody (of the GameObject) is not kinetic when the mouse button is up. 

        StartCoroutine(Release());
        //Allows functions to opporate when the mouse button is released after being pressed down.
    }

    IEnumerator Release()
        //Creates a separation between the rest of the script and this code, so this only happens once the rest of the code has happened.
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
        yield return new WaitForSeconds(0.1f);
        //Applies a SpringJoint2D to the GameObject after the ball has been released, which keeps the GameObjects apart once
        //it is in its projectile.
        BandScript.BandVisible = 1;
        //Makes the band visible before the ball has been released.


        yield return new WaitForSeconds(3f);
        if (NextBall !=null)
        {
            NextBall.SetActive(true);
        //Puts the next ball into the catapault once the previous ball has been launched. 
            BandScript.BandVisible = 0;
            //Makes the band dissappear after it has released the ball.
        }

    }
}
