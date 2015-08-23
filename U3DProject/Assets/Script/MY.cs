using UnityEngine;
using System.Collections;

public class MY<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            lock (mLocker)
            {
                if (mGo == null)
                {
                    mGo = new GameObject();
                    mGo.name = "Singleton";
                    GameObject.DontDestroyOnLoad(mGo);
                }
                if (null == mInstance)
                {
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
