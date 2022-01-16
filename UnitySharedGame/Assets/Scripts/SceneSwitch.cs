using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    GameObject PlayObj;
    public MoveToScene ChangeScene;
    public enum MoveToScene { Kitchen=0, GreenHouse, ColdStorage, Outside };

    public static string GetSceneName(MoveToScene moveToScene)
    {
        switch (moveToScene)
        {
            case MoveToScene.Kitchen:
                return "Kitchen";
            case MoveToScene.GreenHouse:
                return "GreenHouse";
            case MoveToScene.ColdStorage:
                return "ColdStorage";
            case MoveToScene.Outside:
                return "Outside";
            default:
                return "InvalidScene";
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync((int)ChangeScene);
        switch (ChangeScene)
        {
            case MoveToScene.Kitchen:
                if (currentSceneName.Equals(GetSceneName(MoveToScene.GreenHouse)))
                {
                    SetPlayerPosition(-5.5f, -0.75f);
                }
                else if (currentSceneName.Equals(GetSceneName(MoveToScene.ColdStorage)))
                {
                    SetPlayerPosition(6.5f, 5f);
                }
                else if (currentSceneName.Equals(GetSceneName(MoveToScene.Outside)))
                {
                    SetPlayerPosition(.5f, -2f);
                }
                break;
            case MoveToScene.GreenHouse:
                SetPlayerPosition(14f, 4.25f);
                break;
            case MoveToScene.Outside:
                SetPlayerPosition(-0.5f, 0.8f);
                break;
            case MoveToScene.ColdStorage:
                SetPlayerPosition(-6.5f, -4f);
                break;
            default:
                break;
        }
    }

    private void SetPlayerPosition(float x, float y)
    {
        PlayObj = GameObject.Find("Chef_Player");
        Vector3 PlyrPos = PlayObj.gameObject.transform.position;
        PlyrPos.x = x;
        PlyrPos.y = y;
        Debug.Log(PlyrPos.x + "," + PlyrPos.y);
        PlayObj.gameObject.transform.position = PlyrPos;
    }
}
