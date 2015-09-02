using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class StatNpc : MonoBehaviour
{
    public Vector3 orignalPos;
    public int npcid;
    [SerializeField, Range(5,25)]
    public float aiRange = 25f;
    public string ai;
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
