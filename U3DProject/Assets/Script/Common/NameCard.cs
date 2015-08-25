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
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = camera.transform.rotation;
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
