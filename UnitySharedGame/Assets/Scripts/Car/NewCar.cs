using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCar : MonoBehaviour
{
    GameObject car;
    Vector3 carPosition;
    GameObject[] carsTotal;
    int carCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("Car");
        carPosition = car.transform.position;
        UpdateCarArray();
    }
    

    // Update is called once per frame
    void Update()
    {
        

        if (carsTotal.Length > 0)
        {
            UpdateCarArray();

            if (Input.GetKeyDown(KeyCode.N))
            {
                carPosition = car.transform.position;
                carPosition.x -= 8;
                Instantiate(car, carPosition, Quaternion.identity);
                UpdateCarArray();
                car = carsTotal[carsTotal.Length - 1];
                car.gameObject.name = "Car" + carCount;
                carCount++;

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
    
    void UpdateCarArray()
    {
       carsTotal = GameObject.FindGameObjectsWithTag("Car");
    }
}
