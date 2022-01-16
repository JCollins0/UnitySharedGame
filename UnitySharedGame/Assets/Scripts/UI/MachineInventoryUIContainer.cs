using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInventoryUIContainer : MonoBehaviour
{
    public static MachineInventoryUIContainer instance;
    private int openCount = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static void UpdateState(bool activate)
    {
        if (activate)
        {
            ActivateMachineInventoryUI();
        }
        else
        {
            DeActivateMachineInventoryUI();
        }
    }

    public static void ActivateMachineInventoryUI()
    {
        instance.openCount++;
        instance.gameObject.SetActive(true);
    }

    public static void DeActivateMachineInventoryUI()
    {
        instance.openCount--;
        if(instance.openCount == 0)
        {
            instance.gameObject.SetActive(false);
        }
    }
}

