using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineUIManager : MonoBehaviour
{
    public GameObject machineUIRef;
    public List<GameObject> machineUIPrefabs;

    public Dictionary<int, GameObject> machineUIMap;
    public static MachineUIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        machineUIMap = new Dictionary<int, GameObject>();
        for(int i = 0; i < machineUIPrefabs.Count; i++)
        {
            var uiPrefab = machineUIPrefabs[i];
            var obj = Instantiate(uiPrefab, machineUIRef.transform);
            int id = uiPrefab.GetComponent<MachineUI>().id;
            machineUIMap.Add(id, obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
