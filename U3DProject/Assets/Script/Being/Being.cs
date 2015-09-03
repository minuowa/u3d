using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MissionMgr))]
//[RequireComponent(typeof(NameCard))]
[RequireComponent(typeof(StatBeing))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OnDamage))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(DamageReceiver))]
[RequireComponent(typeof(AnimationCallBack))]

[DisallowMultipleComponent]

[behaviac.TypeMetaInfo("生物","基本生物")]
public class Being : behaviac.Agent
{
    [HideInInspector]
    public NameCard nameCard;
    public StatBeing statBeing
    {
        get
        {
            return GetComponent<StatBeing>();
        }
    }
    public MissionMgr missionMgr
    {
        get
        {
            return GetComponent<MissionMgr>();
        }
    }
    public Animator animator
    {
        get
        {
            return GetComponent<Animator>();
        }
    }
    public NavMeshAgent pathFinder
    {
        get
        {
            return GetComponent<NavMeshAgent>();
        }
    }
    [HideInInspector]
    public DamageReceiver damageReceiver
    {
        get
        {
            return GetComponent<DamageReceiver>();
        }
    }

    public float rotateSpeed = 3.0f;
    [HideInInspector]
    public Being mTarget;


    protected List<Being> mBeings;

    public Being()
    {
        mBeings = new List<Being>();

    }

    public virtual void Awake()
    {
        pathFinder.angularSpeed = 720;
        pathFinder.acceleration = 1000;
    }
    public virtual void Start()
    {
        UpdateHead();
        base.Init();
    }
    void UpdateHead()
    {
        if (!nameCard)
        {
            Transform nameposObj = gameObject.transform.Find("namepos");
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
                nameCard = nameCardObj.GetComponent<NameCard>();
            }
        }

        if (nameCard)
        {
            nameCard.displayName = gameObject.name;
        }

    }
    public virtual void Update()
    {
        btexec();
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
    public void Attack(Being obj, int skillid = (int)Default.NormalAttack)
    {
        if (!obj)
            return;

        Skill.Executor param = new Skill.Executor();
        param.skillID = skillid;
        param.receiver = obj;
        this.Do(ActionID.Skill, param);
    }
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
                    missionMgr.Add(para, MissionOption.ClearList);
                }
                break;
            case ActionID.Skill:
                {
                    Skill.Executor param = (Skill.Executor)para;
                    GroundMoveParam moveParam = new GroundMoveParam();
                    moveParam.receiver = param.receiver;
                    moveParam.sender = this;
                    moveParam.miniDistance = param.data.distance;
                    missionMgr.Add(moveParam, MissionOption.ClearList);
                    missionMgr.Add(param);
                }
                break;
        }
    }
    #region 技能
    public Vector3 GetArcherShotPos()
    {
        Vector3 v = transform.localPosition;
        CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
        if (collider)
        {
            v.y += collider.height * 0.8f;
            return v;
        }
        return v;
    }
    #endregion
    #region AI
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
    public Being Get(int index)
    {
        return mBeings[index];
    }
    public int BeingCountAround()
    {
        return mBeings.Count;
    }
    [behaviac.MethodMetaInfo()]
    public bool IsMoving()
    {
        GroundMove move = missionMgr.GetMission<GroundMove>();
        if (move && !move.completed)
            return true;
        return false;
    }
    [behaviac.MethodMetaInfo()]
    public bool IsSkilling()
    {
        MissionSkill move = missionMgr.GetMission<MissionSkill>();
        if (move && !move.completed)
            return true;
        return false;
    }
    [behaviac.MethodMetaInfo()]
    public bool IsBeingAround()
    {
        return BeingCountAround() > 0;
    }
    [behaviac.MethodMetaInfo()]
    public bool IsDoingTask()
    {
        return missionMgr.IsDoing();
    }
    [behaviac.MethodMetaInfo()]
    public bool HasTarget()
    {
        return mTarget != null;
    }
    [behaviac.MethodMetaInfo()]
    public void GoToFirst()
    {
        GroundMoveParam moveParam = new GroundMoveParam();
        Being being = mBeings[0];
        moveParam.rawpos = being.transform.position;
        moveParam.visible = true;
        Do(ActionID.MoveTo, moveParam);
    }
    #endregion
}
