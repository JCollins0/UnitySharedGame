using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FoodDefintions fd = new FoodDefintions(); //temporary
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.Translate(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.Translate(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(Vector3.right);
        }
    }
}
