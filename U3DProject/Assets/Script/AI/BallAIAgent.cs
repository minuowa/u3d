//using UnityEngine;
//using System.Collections;

//[behaviac.TypeMetaInfo("球","球")]
//public class BallAIAgent : behaviac.Agent
//{
//    [behaviac.MemberMetaInfo()]
//    public float speed = 200;

//    public int hp = 1000;

//    public float scale;

//    void Start()
//    {
//        base.Init();
//        MY<AISystem>.Instance.Load(this, MY<AISystem>.Instance.ballAI);
//        rigidbody.velocity = new Vector3(0.5f, 0, 1).normalized * speed;
//    }

//    void Awake()
//    {
//    }

//    void Update()
//    {
//        btexec();
//    }

//    [behaviac.MethodMetaInfo()]
//    public behaviac.EBTStatus RandomStart()
//    {
//        return behaviac.EBTStatus.BT_RUNNING;
//    }
//    [behaviac.MethodMetaInfo()]
//    public void Back()
//    {

//    }
//    void OnCollisionEnter(Collision collision)
//    {
//        foreach (ContactPoint contact in collision.contacts)
//        {
//            Debug.DrawRay(contact.point, contact.normal, Color.red);
//            if (contact.otherCollider.GetType() != typeof(TerrainCollider))
//            {
//                rigidbody.AddForce(contact.normal.normalized * rigidbody.mass * 100, ForceMode.Impulse);
//            }
//        }
//    }

//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawLine(rigidbody.position, rigidbody.position + 10000 * speed * rigidbody.velocity);
//    }
//    [behaviac.MethodMetaInfo]
//    public behaviac.EBTStatus IsInjured()
//    {
//        return behaviac.EBTStatus.BT_SUCCESS;
//    }
//}
