using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public int moveSpeed;
    public int rotateSpeed;
    // Use this for initialization
    private Transform myTrans;
    void Awake()
    {
        myTrans = transform;
    }
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = Fun.GetPostion(target);
        Debug.DrawLine(targetPos, myTrans.transform.position, Color.red);

        myTrans.rotation = Quaternion.Slerp(myTrans.rotation
     , Quaternion.LookRotation(targetPos - myTrans.position)
     , Time.deltaTime * rotateSpeed);
        myTrans.position += myTrans.forward * Time.deltaTime;
    }
}
