using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;   
    }

    public Action<int> onInventoryChange;
    public void InventoryChange(int id)
    {
        onInventoryChange?.Invoke(id);
    }
}
