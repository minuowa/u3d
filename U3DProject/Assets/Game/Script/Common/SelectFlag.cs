using UnityEngine;
using System.Collections;

public class SelectFlag : MonoBehaviour {

    GameObject _goundFlag;
    // Use this for initialization
	void Start () {
        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/selectFlag", typeof(GameObject));
        _goundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        _goundFlag.transform.parent = gameObject.transform;
        _goundFlag.transform.localPosition = preGroundObj.transform.localPosition;
        _goundFlag.transform.localScale = preGroundObj.transform.localScale;
        _goundFlag.SetActive(true);
	}
	
	// Update is called once per frame
	void OnDestroy () {
        GameObject.Destroy(_goundFlag);
	}
}
