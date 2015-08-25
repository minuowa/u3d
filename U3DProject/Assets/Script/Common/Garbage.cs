using UnityEngine;
using System.Collections;

public class Garbage
{
    public GameObject root;
    public static Garbage Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new Garbage();
            }
            return mInstance;
        }

    }
    static Garbage mInstance;

    public Garbage()
    {
        root = new GameObject("garbage");
    }
}
