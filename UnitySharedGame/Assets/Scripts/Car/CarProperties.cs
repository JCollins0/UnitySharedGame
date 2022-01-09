using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarProperties : MonoBehaviour
{
    int orderTotal;
    int carColor;

    public Sprite[] carSprites = new Sprite[6];

    Transform[] orderBubbles = new Transform[3];
    
    
    // Start is called before the first frame update
    void Start()
    {
        orderTotal = (int)Random.Range(1,4);
        carColor = (int)Random.Range(0, 6);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = carSprites[carColor];

        Debug.Log("OrderTotal: " + orderTotal);

        //puts order bubbles in array
        orderBubbles[0] = this.gameObject.transform.GetChild(0);
        orderBubbles[1] = this.gameObject.transform.GetChild(1);
        orderBubbles[2] = this.gameObject.transform.GetChild(2);

        //prints orderbubbles in arry
        for (int i=0; i<orderBubbles.Length; i++)
        {
            if(i == orderTotal - 1)
            {
                orderBubbles[i].gameObject.SetActive(true);
            }
            else
            {
                orderBubbles[i].gameObject.SetActive(false);
            }
            Debug.Log(orderBubbles[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void RandomGenerator()
    {

    }
}
