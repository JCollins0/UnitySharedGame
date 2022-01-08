using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb2D;
    Vector3 playerScale;

    public float forceX = 0.2f;
    public float forceY = 0.2f;

    Animator playerAnim;
    int WalkState = 0;
    private static Player playerInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnim = this.gameObject.GetComponent<Animator>();
        playerScale = this.gameObject.transform.localScale;
        playerAnim.SetInteger("PlayerWalkState", WalkState);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        rb2D.velocity = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            //player moves up
            playerScale.x = 1;
            WalkState = 3;
            rb2D.velocity = new Vector2(0, forceY);            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //player moves right
            playerScale.x = 1;
            WalkState = 1;
            rb2D.velocity = new Vector2(forceX, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            //player moves down
            playerScale.x = 1;
            WalkState = 2;
            rb2D.velocity = new Vector2(0, -forceY);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //player moves left
            playerScale.x = -1;
            WalkState = 1;
            this.gameObject.GetComponent<Transform>().localScale = playerScale;
            rb2D.velocity = new Vector2(-forceX, 0);
        }
        else
        {
            playerScale.x = 1;
            WalkState = 0;
            rb2D.velocity = new Vector2(0, 0);
        }

       
        this.gameObject.transform.localScale = playerScale;
        playerAnim.SetInteger("PlayerWalkState", WalkState);
    }

    
}
