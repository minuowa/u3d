using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public Transform target;
    public int moveSpeed;
    public int rotateSpeed;
	// Use this for initialization
    private Transform myTrans;
    void Awake()
    {
        myTrans = transform;
    }
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
 
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(target.transform.position, myTrans.transform.position,Color.red);

        myTrans.rotation = Quaternion.Slerp(myTrans.rotation
     , Quaternion.LookRotation(target.position - myTrans.position)
     , Time.deltaTime * rotateSpeed);
        myTrans.position += myTrans.forward * Time.deltaTime;
    }
}
