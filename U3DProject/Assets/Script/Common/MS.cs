using UnityEngine;
using System.Collections;

public class MS<T> : MonoBehaviour where T : MonoBehaviour
{
    public static string objName = "_Singleton_";
    public static T Instance
    {
        get
        {
            lock (mLocker)
            {
                if (!mGo)
                {
                    mGo = GameObject.Find(objName);
                    if (!mGo)
                    {
                        mGo = new GameObject(objName);
                        GameObject.DontDestroyOnLoad(mGo);
                    }
                }
                if (!mInstance)
                {
                    mInstance = mGo.GetComponent<T>();
                    if (!mInstance)
                        mInstance = mGo.AddComponent<T>();
                }
            }
            return mInstance;
        }
    }
    static T mInstance;
    static GameObject mGo;
    static object mLocker = new object();
}
