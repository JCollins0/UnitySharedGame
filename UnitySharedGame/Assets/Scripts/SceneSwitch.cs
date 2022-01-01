using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    GameObject PlayObj;
    public MoveToScene ChangeScene = new MoveToScene();
    public enum MoveToScene { Kitchen, Greenhouse, ColdStorage };

    // Start is called before the first frame update
    void Start()
    {
        PlayObj = GameObject.Find("Chef_Player");
        DontDestroyOnLoad(PlayObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Vector3 PlyrPos = PlayObj.gameObject.transform.position;
        Scene CurrentScene = SceneManager.GetActiveScene();

           if (ChangeScene.ToString() == "Kitchen") {

               if (CurrentScene.name == "GreenHouse")
               {
                   PlyrPos.x = -5.5f;
                   PlyrPos.y = -0.75f;
               }
               else if(CurrentScene.name == "ColdStorage")
               { 
                   PlyrPos.x = 6.5f;
                   PlyrPos.y = -0.8f;
               }
               SceneManager.LoadScene(0);  
               Debug.Log(PlyrPos.x + "," + PlyrPos.y);
           }
           else if (ChangeScene.ToString() == "Greenhouse")
           {
               SceneManager.LoadScene(1);
               PlyrPos.x = 14f;
               PlyrPos.y = 4.25f;

           }
           else if (ChangeScene.ToString() == "ColdStorage")
           {
               SceneManager.LoadScene(2);

           }
        PlayObj.gameObject.transform.position = PlyrPos;
    }
}
