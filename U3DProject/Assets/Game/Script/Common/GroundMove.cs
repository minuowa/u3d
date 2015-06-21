using UnityEngine;
using System.Collections;

public class GroundMove :  IMission
{
    public int speed = 4;
    public float miniDistance = 0.7f;


    public Vector3 target;

    private bool _finding = false;
    Duration _duration;
    GameObject _goundFlag;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        base.Begin();

        _finding = true;
        _duration = new Duration();
        _duration.total = 0.5f;

        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
        _goundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        _goundFlag.transform.localPosition = target;
        _goundFlag.transform.localScale = preGroundObj.transform.localScale;
        _goundFlag.SetActive(true);

        animator = GetComponentInChildren<Animator>();
        animator.SetInteger(BeingAction.action, BeingAction.Run1);

        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        if (collider)
            target.y += (collider.height + collider.radius)*0.5f;

        _completed = false;
    }

    void OnDestroy()
    {
        Destroy(_goundFlag);
    }
    // Update is called once per frame
    void EndFinding()
    {
        _completed = true;
        _finding = false;
        _duration.Reset();
        animator.SetInteger(BeingAction.action, BeingAction.Idle1);
        GameObject.Destroy(this);
    }
    public override void Update()
    {
        if (_finding)
        {
            Vector3 mypos = animator.rootPosition;
            Quaternion myrotation = transform.rotation;
            Vector3 v0 = mypos;
            Vector3 v1 = target;
            v0.y = 0;
            v1.y = 0;
            if (Vector3.Distance(mypos, target) < miniDistance
                || Vector3.Distance(v0, v1) < 0.01f)
            {
                EndFinding();
            }
            else
            {
                Debug.DrawLine(target, mypos, Color.green);

                CharacterController ctrler = GetComponentInParent<CharacterController>();
                if (ctrler)
                {
                    Vector3 vt = target;
                    vt.y = mypos.y;

                    Quaternion qfrom = myrotation;
                    Quaternion qto = Quaternion.LookRotation(vt - mypos);
                    //animator.rootRotation = qto;
                    transform.rotation = qto;
                    //animator.rootRotation = Quaternion.Slerp(qfrom, qto, _duration.progress);

                    Vector3 dir = target - mypos;
                    dir.Normalize();
                    ctrler.SimpleMove(dir * speed);

                    if (Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.z, 2) < 0.1f)
                    {
                        EndFinding();
                    }
                }
                if (_duration.Advance(Time.deltaTime))
                {
                }
            }
        }
    }

}
