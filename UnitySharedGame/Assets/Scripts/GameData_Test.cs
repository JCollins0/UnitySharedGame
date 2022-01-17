using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Test : MonoBehaviour
{
    bool switchingScenesHasBegun = false;

    private static GameData_Test GDInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (GDInstance == null)
        {
            GDInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public bool returnSwitchScene()
    {
        return switchingScenesHasBegun;
    }

    // Start is called before the first frame update
    void Start()
    {

        switchingScenesHasBegun = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
