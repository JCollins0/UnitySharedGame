using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    Animator plantGrowth;

    public enum TypeOfCrop { FIELD = 1, ROOT = 2, TREE = 3, WATER = 4 };

    [Header("CROP PROPERTIES")]
    public TypeOfCrop CropType;

    public string Product;

    GameData_Test gameData;

    private void Awake()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData_Test>();
        if (gameData.returnSwitchScene() == false)
        {
            plantGrowth = this.gameObject.GetComponent<Animator>();
            plantGrowth.SetInteger("fieldType", (int)CropType);
            Debug.Log("Plant Type is: " + CropType + " " + (int)CropType);

        }

    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
