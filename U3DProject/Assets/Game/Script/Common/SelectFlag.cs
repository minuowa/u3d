using UnityEngine;
using System.Collections;

public class SelectFlag : MonoBehaviour {

    GameObject mGroundFlag;
    // Use this for initialization
	void Start () {
        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/selectFlag", typeof(GameObject));
        mGroundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        mGroundFlag.transform.parent = gameObject.transform;
        mGroundFlag.transform.localPosition = preGroundObj.transform.localPosition;
        mGroundFlag.transform.localScale = preGroundObj.transform.localScale;
        mGroundFlag.SetActive(true);
	}
	
	// Update is called once per frame
	void OnDestroy () {
        GameObject.Destroy(mGroundFlag);
	}
}
