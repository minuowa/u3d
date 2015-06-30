using UnityEngine;
using System.Collections;

public class Being : MonoBehaviour
{

    public string displayName = string.Empty;
    protected NameCard _nameCard;
    protected BeingStat _beingStat;
    protected MissionMgr _missionMgr;
    protected Animator _animator;
    protected BeingState _state;

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
                    _missionMgr.Add(move, true);
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
                    _missionMgr.Add(move, true);

                    Skill.Executor executor = new Skill.Executor();
                    executor.skillid = param.skillID;
                    executor.actor = this;
                    executor.victim = param.receiver;
                    MissionSkill missskill = new MissionSkill();
                    missskill.executor = executor;
                    _missionMgr.Add(missskill, true);
                }
                break;
        }
    }
    public virtual void Start()
    {
        _nameCard = gameObject.AddComponent<NameCard>();
        _beingStat = gameObject.AddComponent<BeingStat>();
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            _animator = gameObject.AddComponent<Animator>();
        _missionMgr = gameObject.AddComponent<MissionMgr>();
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
