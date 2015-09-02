using UnityEngine;
using System.Collections;
[RequireComponent(typeof(StatNpc))]
[AddComponentMenu("RPG/Obj/Npc")]
[behaviac.TypeMetaInfo("Npc", "Npc")]
[DisallowMultipleComponent]
public class Npc : Being
{
    public StatNpc statNpc
    {
        get
        {
            return GetComponent<StatNpc>();
        }
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        ReloadAI();
    }
    public override void Awake()
    {
        base.Awake();
        pathFinder.speed = 0.5f;
        pathFinder.angularSpeed = 360;
        pathFinder.acceleration = 500;
    }
    // Update is called once per frame
    public override void Update()
    {
        UpdateAround();
        UpdateTarget();
        base.Update();
    }

    void UpdateAround()
    {
        if (string.IsNullOrEmpty(statBeing.ai))
            return;
        mBeings.Clear();
        Being[] alls = (Being[])GameObject.FindObjectsOfType(typeof(Being));
        foreach (var be in alls)
        {
            if (be.Equals(this))
                continue;
            if (Vector3.Distance(be.transform.localPosition, transform.localPosition) < statNpc.aiRange)
                mBeings.Add(be);
        }
    }
    public void UpdateTarget()
    {
        if (mTarget != null)
        {
            //目标远离了
            if (mBeings.IndexOf(mTarget) == -1)
                mTarget = null;
        }
        if (mTarget == null)
        {
            mTarget = damageReceiver.firstAttacker;
        }
        if (mTarget == null)
        {
            if (mBeings.Count > 0)
                mTarget = mBeings[0];
        }
    }
    [behaviac.MethodMetaInfo()]
    public behaviac.EBTStatus RandomStart()
    {
        return behaviac.EBTStatus.BT_RUNNING;
    }
    #region AI

    [behaviac.MethodMetaInfo()]
    public behaviac.EBTStatus RandomMove()
    {
        float range = Random.Range(3.0f, statNpc.aiRange);
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float x = range * Mathf.Cos(angle) + statNpc.orignalPos.x;
        float z = range * Mathf.Sin(angle) + statNpc.orignalPos.z;
        Terrain terrain = Terrain.activeTerrain;
        x = Mathf.Clamp(x, 0f, terrain.terrainData.size.x);
        z = Mathf.Clamp(z, 0f, terrain.terrainData.size.z);
        GroundMoveParam param = new GroundMoveParam();
        param.rawpos = new Vector3(x, statNpc.orignalPos.y, z);
        param.miniDistance = 0.7f;
        this.Do(ActionID.MoveTo, param);
        return behaviac.EBTStatus.BT_SUCCESS;
    }
    [behaviac.MethodMetaInfo()]
    public bool Stand()
    {
        if (mTarget != null)
        {
            return false;
        }

        return mTarget != null;
    }
    [behaviac.MethodMetaInfo()]
    public behaviac.EBTStatus AttackTarget()
    {
        Attack(mTarget);
        return behaviac.EBTStatus.BT_SUCCESS;
    }

    public bool IsEnemy(int idx)
    {
        return GroupManager.IsEnemy(statBeing.group, Get(idx).statBeing.group);
    }
    public int GetAroundHp(int idx)
    {
        return Get(idx).GetHp();
    }
    public int GetAroundHpPercent(int idx)
    {
        return Get(idx).GetHpPercent();
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

    #endregion






    void ReloadAI()
    {
        if (!string.IsNullOrEmpty(statNpc.ai))
            MS<AISystem>.Instance.Load(this, statNpc.ai);
    }
}
