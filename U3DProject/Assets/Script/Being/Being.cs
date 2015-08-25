using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MissionMgr))]
//[RequireComponent(typeof(NameCard))]
[RequireComponent(typeof(StatBeing))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OnDamage))]
[RequireComponent(typeof(NavMeshAgent))]

[behaviac.TypeMetaInfo("生物","带AI的生物")]
public class Being : behaviac.Agent
{
    protected NameCard mNameCard;
    protected StatBeing mStatBeing;
    protected MissionMgr mMissionMgr;
    protected Animator mAnimator;
    protected NavMeshAgent mPathFinder;

    public float rotateSpeed = 3.0f;
    [HideInInspector]
    public Being mTarget;


    List<Being> mBeings;

    public Being()
    {
        mBeings = new List<Being>();
    }


    void Awake()
    {
    }

    public virtual void Update()
    {
        UpdateAround();
        btexec();
    }
    void UpdateAround()
    {
        if (string.IsNullOrEmpty(mStatBeing.ai))
            return;
        mBeings.Clear();
        Being[] alls = (Being[])GameObject.FindObjectsOfType(typeof(Being));
        foreach (var be in alls)
        {
            if (be.Equals(this))
                continue;
            if (Vector3.Distance(be.transform.localPosition, transform.localPosition) < mStatBeing.aiRange)
                mBeings.Add(be);
        }
    }
    [behaviac.MethodMetaInfo()]
    public behaviac.EBTStatus RandomStart()
    {
        return behaviac.EBTStatus.BT_RUNNING;
    }
    #region AI
    Being Get(int index)
    {
        return mBeings[index];
    }
    [behaviac.MethodMetaInfo()]
    public bool IsBeingAround()
    {
        return BeingCountAround() > 0;
    }

    [behaviac.MethodMetaInfo()]
    public void GoToFirst()
    {
        GroundMoveParam moveParam = new GroundMoveParam();
        Being being = mBeings[0];
        moveParam.rawpos = being.transform.position;
        moveParam.auto = true;
        Do(ActionID.MoveTo, moveParam);
    }
    public int BeingCountAround()
    {
        return mBeings.Count;
    }
    public bool IsEnemy(int idx)
    {
        return GroupManager.IsEnemy(mStatBeing.group, Get(idx).mStatBeing.group);
    }
    public int GetAroundHp(int idx)
    {
        return Get(idx).GetHp();
    }
    public int GetAroundHpPercent(int idx)
    {
        return Get(idx).GetHpPercent();
    }
    public int GetHp()
    {
        return 1;
    }
    public int GetHpPercent()
    {
        return 1;
    }
    public int GetTargetType()
    {
        return 0;
    }
    public string GetTargetName()
    {
        if (mTarget)
            return mTarget.name;
        return string.Empty;
    }
    public int GetTargetNpcID()
    {
        return 0;
    }
    public string GetEnemyName(int idx)
    {
        return Get(idx).name;
    }
    public float GetTargetDistance()
    {
        return Vector3.Distance(mTarget.transform.localPosition, transform.localPosition);
    }
    public float GetEnemyDistance(int idx)
    {
        return Vector3.Distance(Get(idx).gameObject.transform.localPosition, transform.localPosition);
    }
    public int GetState()
    {
        return 0;
    }
    public void SetAction(int id)
    {

    }
    public void Talk(string str)
    {

    }
    public void Move(Vector3 tar)
    {

    }
    public void SetTarget(int idx)
    {
        mTarget = Get(idx);
    }
    public void Attack(int idx)
    {
    }
    #endregion
    public void Do(ActionID action, IMissionParam para)
    {
        para.sender = this;

        switch (action)
        {
            case ActionID.SelectTarget:
                {
                    SelectParam param = (SelectParam)para;
                    if (mTarget != null)
                        mTarget.Unselect();
                    param.receiver.Select();
                    mTarget = param.receiver;
                }
                break;
            case ActionID.MoveTo:
                {
                    GroundMoveParam param = para as GroundMoveParam;
                    GroundMove move = new GroundMove();

                    mMissionMgr.Add(move, param, MissionOption.SetParam);
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
                    mMissionMgr.Add(move, moveParam, MissionOption.SetParam);

                    Skill.Executor executor = new Skill.Executor();
                    executor.skillid = param.skillID;
                    executor.sender = this;
                    executor.receiver = param.receiver;
                    MissionSkill missskill = new MissionSkill();
                    mMissionMgr.Add(missskill, executor);
                }
                break;
        }
    }
    public virtual void Start()
    {
        mStatBeing = gameObject.GetComponent<StatBeing>();
        mAnimator = GetComponentInChildren<Animator>();
        mMissionMgr = gameObject.GetComponent<MissionMgr>();
        mPathFinder = gameObject.GetComponent<NavMeshAgent>();
        mPathFinder.angularSpeed = 720;
        mPathFinder.acceleration = 1000;
        UpdateHead();

        base.Init();
        ReloadAI();
    }
    void UpdateHead()
    {
        if (!mNameCard)
        {
            Transform nameposObj = gameObject.transform.Find("namePos");
            GameObject nameCardObj = AResource.Instance("Prefabs/nameCard/nameRoot");
            if (nameCardObj)
            {
                if (nameposObj)
                {
                    nameCardObj.transform.parent = nameposObj.transform;
                }
                else
                {
                    nameCardObj.transform.parent = transform;
                }
                nameCardObj.transform.localPosition = Vector3.zero;
                nameCardObj.transform.localScale = new Vector3(7, 7, 7f);
                mNameCard = nameCardObj.GetComponent<NameCard>();
            }
        }

        if (mNameCard)
        {
            mNameCard.displayName = gameObject.name;
        }

    }
    void ReloadAI()
    {
        if(!string.IsNullOrEmpty(mStatBeing.ai))
            MS<AISystem>.Instance.Load(this, mStatBeing.ai);
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
