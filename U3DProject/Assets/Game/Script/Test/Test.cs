using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public GameObject obj;

    public void SetPostion(Vector3 pos)
    {
        if (obj)
            obj.transform.position = pos;
    }
	// Use this for initialization
	void Start () {
        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
        obj = (GameObject)GameObject.Instantiate(preGroundObj);
        obj.transform.parent = gameObject.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = preGroundObj.transform.localScale;
        obj.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
