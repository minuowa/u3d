using UnityEngine;
using System.Collections;

public class GroundMove : MonoBehaviour {
    public int speed = 9;
    public float miniDistance = 0.7f;


    public Vector3 target;

    private bool _finding = false;

    Duration _duration;
    GameObject _goundFlag;
	// Use this for initialization
	void Start () {
        _finding = true;
        _duration = new Duration();
        _duration.total = 1.5f;
        GameObject preGroundObj=(GameObject)Resources.Load("Prefabs/GameObject/groundFlag",typeof(GameObject));
        _goundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        _goundFlag.transform.localPosition = target;
        _goundFlag.transform.localScale = preGroundObj.transform.localScale;
        _goundFlag.SetActive(true);
	}

    void OnDestroy()
    {
        Destroy(_goundFlag);
    }
    // Update is called once per frame
    void EndFinding()
    {
        Animation anim = GetComponentInParent<Animation>();
        _finding = false;
        if (anim != null)
            anim.CrossFade("idle");
        GameObject.Destroy(this);
    }
    void Update()
    {
        if (_finding)
        {
            Vector3 v0 = transform.position;
            Vector3 v1 = target;
            v0.y = 0;
            v1.y = 0;
            if (Vector3.Distance(transform.position, target) < miniDistance
                || Vector3.Distance(v0, v1) < 0.01f)
            {
                EndFinding();
            }
            else
            {
                Debug.DrawLine(target, transform.position, Color.green);

                CharacterController ctrler = GetComponentInParent<CharacterController>();
                if (ctrler)
                {
                    Vector3 vt = target;
                    vt.y = transform.position.y;

                    Quaternion qfrom = transform.rotation;
                    Quaternion qto = Quaternion.LookRotation(vt - transform.position);
                    transform.rotation = Quaternion.Slerp(qfrom, qto, _duration.progress);

                    Vector3 dir = target - transform.position;
                    dir.Normalize();
                    ctrler.SimpleMove(dir * speed);

                    if (Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.z, 2) < 0.1f)
                    {
                        EndFinding();
                    }
                    else
                    {
                        Animation anim = GetComponentInParent<Animation>();
                        if (anim != null)
                            anim.CrossFade("run");
                    }
                }
                if (_duration.Advance(Time.deltaTime))
                {
                    _duration.Reset();
                }
            }
        }
    }
}
