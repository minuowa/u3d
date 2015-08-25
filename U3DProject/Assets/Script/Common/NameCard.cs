using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class NameCard : MonoBehaviour
{
    public string displayName
    {
        get
        {
            return mDisplayName;
        }
        set
        {
            mDisplayName = value;
            Fun.SetFirstChild(gameObject, "name", mDisplayName);
        }
    }
    string mDisplayName;
    Camera mCamera;
    void Start()
    {
        mCamera = Camera.main;
    }

    void Update()
    {
        transform.rotation = mCamera.transform.rotation;
    }
    void OnBecameVisible()
    {
    }
    void OnBecameInvisible()
    {
    }
    void OnGUI()
    {
    }
}
