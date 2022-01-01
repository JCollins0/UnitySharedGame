using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DontDestroyOnLoad(this.gameObject);
        Vector3 PlyrPos = this.gameObject.transform.position;
        PlyrPos.x = 0;
        PlyrPos.y = 0;
        this.gameObject.transform.position = PlyrPos;
    }
}
