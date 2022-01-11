using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    bool carReadyToPickUp = false;
    bool foodDelivered = false;
    bool clearToMove = true;

    Vector3 CarPos;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        CarPos = this.gameObject.transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (clearToMove == true)
        {
            CarPos = this.gameObject.transform.position;

            if (carReadyToPickUp == false) //add ClearToMove Bool - check if car obj in front of it
            {
                //move car to pick up spot
                Debug.Log("Car moving to pickup spot");
                foodDelivered = false;

                if (CarPos.x >= -0.5f)
                {
                    //stop car at pickup spot
                    Debug.Log("Car is stopping");
                    rb2D.velocity = new Vector2(0, 0);
                    carReadyToPickUp = true;

                }
                else
                {
                    //keep moving car
                    rb2D.velocity = new Vector2(1.5f, 0);
                    //this.gameObject.transform.position = CarPos;
                }

            }
            else
            {
                //car is waiting at pickup spot - put in collision detection with player - check if food delivered - if yes (move off screen), if no (continue not moving)

                if (foodDelivered == true)
                {
                    rb2D.velocity = new Vector2(1.5f, 0);
                    this.gameObject.transform.position = CarPos;
                    if (CarPos.x > 9.5)
                    {
                        //car can be destroyed
                        Destroy(this.gameObject);
                    }
                }

            }
        }
        else
        {
            //keep still
            rb2D.velocity = new Vector2(0f, 0);
            Debug.Log("Not clear to move, keep car stopped");
        }


    }

    void FixedUpdate()
    {
        


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        //check if food is correct
        //if yes, food delivered = true
        //if no, food is incorrect, basically do nothing and car stays put
        if (collision.gameObject.name == "Chef_Player" && carReadyToPickUp == true)
        {
            foodDelivered = true;
        }
        if (collision.gameObject.tag == "Car")
        {
            clearToMove = false; //do not move the car
            rb2D.velocity = new Vector2(0, 0); //stopping the car
            Debug.Log("Hit another car, stopping");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            clearToMove = true; //car can move again
            rb2D.velocity = new Vector2(1.5f, 0);
            Debug.Log("Ready To Move again");
        }
    }

}
