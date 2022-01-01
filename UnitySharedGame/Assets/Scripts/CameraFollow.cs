using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Player Chef;

    // Start is called before the first frame update
    void Start()
    {
        Chef = GameObject.Find("Chef_Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraPos = this.transform.position;
        CameraPos = Chef.gameObject.transform.position;
        CameraPos.z = -15;
        this.transform.position = CameraPos;
    }

}
