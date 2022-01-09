using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    int quantity = 1;
    public InventoryManager playerInventory;
    public Item bread;

    // Start is called before the first frame update
    void Start()
    {
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
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (playerInventory.CanAddItem(bread.id))
            {
                playerInventory.AddItem(bread);
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            if (playerInventory.HasItem(bread.id, quantity))
            {
                playerInventory.RemoveItems(bread, quantity);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            quantity--;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            quantity++;
        }
    }
}
