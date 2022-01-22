using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Animator playAnim;
    RaycastHit2D hit;

    Vector3 rayAdjustedPos;

    // Start is called before the first frame update
    void Start()
    {
        playAnim = this.gameObject.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Disable the object's own collider to avoid a false-positive hit

            this.gameObject.GetComponent<Collider2D>().enabled = false;

            if (playAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_UpStill"))
            {
                // Raycast for a hit in the walkUP direction at a maximum distance of 70 units
                hit = Physics2D.Raycast(this.gameObject.transform.position, Vector3.up, 70f, 1 << LayerMask.NameToLayer("Interact"));
            }else if (playAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_DownStill"))
            {
                // Raycast for a hit in the walkDown direction at a maximum distance of 70 units
                hit = Physics2D.Raycast(this.gameObject.transform.position, Vector3.down, 70f, 1 << LayerMask.NameToLayer("Interact"));
            }else if (playAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_SidewaysStill"))
            {
                rayAdjustedPos = this.gameObject.transform.position;
                rayAdjustedPos.y -= 0.4f;
                if(this.gameObject.transform.localScale.x == 1)
                {
                    // Raycast for a hit in the walkSideways pointing right direction at a maximum distance of 70 units
                    hit = Physics2D.Raycast(rayAdjustedPos, Vector3.right, 70f, 1 << LayerMask.NameToLayer("Interact"));
                }
                else
                {
                    // Raycast for a hit in the walkSideways pointing left direction at a maximum distance of 70 units
                    hit = Physics2D.Raycast(rayAdjustedPos, Vector3.left, 70f, 1 << LayerMask.NameToLayer("Interact"));
                }
            }
           

            // Re-enable the object's own collider
            this.gameObject.GetComponent<Collider2D>().enabled = true;

            // Report the hit to the console
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name + " is in front of " + this.gameObject.name);                
                //add code based on thing ray cast hit / player interacted with
            }
        }
    }

    private void FixedUpdate()
    {

       
    }

}
