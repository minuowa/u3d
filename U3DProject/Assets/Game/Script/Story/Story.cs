using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 剧情
/// </summary>
/// 
public enum StoryIndex
{
    Create,
    Move,
    Attack,
    PlayAnimation,
    MoveCamera,
    Talk,
    OpenUI,
    WarGuide,
    DequeueGeneral,
    PlayAdviserSkill,
    Empty,
}

public class Story:MonoBehaviour
{

    public static Story Load(string prefab)
    {
        GameObject preObj = Resources.Load(prefab, typeof(GameObject)) as GameObject;
        GameObject go = GameObject.Instantiate(preObj) as GameObject;
        if (go)
            return go.GetComponent<Story>();
        return null;
    }

    public GameObject Add(string name)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = gameObject.transform;
        go.SetActive(false);
        return go;
    }
    public void Delete(GameObject go)
    {
        GameObject.DestroyImmediate(go);
    }
    

    public static void OnNet(int id)
    {
        switch (id)
        {
            case 0:
                {
                    //GuideManager.Instance.ChangeTo("Button1");
                }
                break;
        }
    }
}
