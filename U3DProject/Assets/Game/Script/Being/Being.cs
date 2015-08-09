using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MissionMgr))]
[RequireComponent(typeof(NameCard))]
[RequireComponent(typeof(StatBeing))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OnDamage))]
[RequireComponent(typeof(NavMeshAgent))]
public class Being : MonoBehaviour
{
    protected NameCard mNameCard;
    protected StatBeing mStatBeing;
    protected MissionMgr mMissionMgr;
    protected Animator mAnimator;
    protected NavMeshAgent mPathFinder;

    public float rotateSpeed = 3.0f;
    [HideInInspector]
    public Being target;

    public Being()
    {
    }
    public void Do(ActionID action, IParam para)
    {
        para.sender = this;

        switch (action)
        {
            case ActionID.SelectTarget:
                {
                    SelectParam param = (SelectParam)para;
                    if (target != null)
                        target.Unselect();
                    param.receiver.Select();
                    target = param.receiver;
                }
                break;
            case ActionID.MoveTo:
                {
                    GroundMoveParam param = para as GroundMoveParam;
                    GroundMove move = new GroundMove();
                    move.param = param;
                    mMissionMgr.Add(move, true);
                }
                break;
            case ActionID.Skill:
                {
                    SkillParam param = (SkillParam)para;

                    GroundMoveParam moveParam = new GroundMoveParam();
                    moveParam.receiver = param.receiver;
                    moveParam.sender = this;
                    moveParam.miniDistance = param.data.distance;
                    GroundMove move = new GroundMove();
                    move.param = moveParam;
                    mMissionMgr.Add(move, true);

                    Skill.Executor executor = new Skill.Executor();
                    executor.skillid = param.skillID;
                    executor.actor = this;
                    executor.victim = param.receiver;
                    MissionSkill missskill = new MissionSkill();
                    missskill.executor = executor;
                    mMissionMgr.Add(missskill, true);
                }
                break;
        }
    }
    public virtual void Start()
    {
        mNameCard = gameObject.GetComponent<NameCard>();
        mStatBeing = gameObject.GetComponent<StatBeing>();
        mAnimator = GetComponentInChildren<Animator>();
        if (mAnimator == null)
            mAnimator = gameObject.GetComponent<Animator>();
        mMissionMgr = gameObject.GetComponent<MissionMgr>();
        mPathFinder = gameObject.GetComponent<NavMeshAgent>();
    }

    public T GetNewComponent<T>() where T : Component
    {
        T com = gameObject.GetComponent<T>();
        if (com != null)
            GameObject.DestroyImmediate(com);
        com = gameObject.AddComponent<T>();
        return com;
    }
    public T TryGetComponent<T>() where T : Component
    {
        T com = gameObject.GetComponent<T>();
        if (com == null)
            com = gameObject.AddComponent<T>();
        return com;
    }
    public void Unselect()
    {
        var select = GetComponent<SelectFlag>();
        if (select != null)
            GameObject.Destroy(select);
    }
    public void Select()
    {
        GetNewComponent<SelectFlag>();
    }
}
