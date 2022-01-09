using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCar : MonoBehaviour
{
    GameObject car;
    Vector3 carPosition;
    GameObject[] carsTotal;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("Car");
        carPosition = car.transform.position;

    }
    

    // Update is called once per frame
    void Update()
    {
        carsTotal = GameObject.FindGameObjectsWithTag("Car");

        if (carsTotal.Length > 0)
        {
            car = carsTotal[carsTotal.Length - 1];
            carPosition = car.transform.position;

            if (Input.GetKeyDown(KeyCode.N))
            {
                carPosition.x -= 5;
                Instantiate(car, carPosition, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(carsTotal[0]);
            }

        }
        else
        {
            Debug.Log("Handled all orders");
            return;
        }

    }

    void FixedUpdate()
    {

        
    }
}
