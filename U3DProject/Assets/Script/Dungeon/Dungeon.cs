using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour {
    public string sceneName = string.Empty;

    Dictionary<int, Being> mBeings;

    public Dungeon()
    {
        mBeings = new Dictionary<int, Being>();
    }

    void Start()
    {
        if (sceneName == string.Empty)
            RandomGenerate();
        else
            ReloadScene();
    }

    public void Clear()
    {
        mBeings.Clear();
    }

    public void RandomGenerate()
    {
        
    }

    bool ReloadScene()
    {
        return true;
    }
}
