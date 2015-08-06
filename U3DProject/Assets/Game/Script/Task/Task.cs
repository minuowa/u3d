using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TaskBase
{
    public int id = 0;
    public int targetCount = 0;
    public int curCount = 0;
    public TaskState state = TaskState.None;

    public bool start
    {
        get
        {
            return state != TaskState.None && state != TaskState.CanGet;
        }
    }

    public bool doing
    {
        get
        {
            return state == TaskState.Doing;
        }
    }
    public bool completed
    {
        get
        {
            return state == TaskState.Done;
        }
    }

    public virtual TaskBase Go()
    {
        return this;
    }
    public virtual float Progress()
    {
        return 0f;
    }
}
public class TaskChild : TaskBase
{
    public int childID;
    public TaskOperType oper;

    public TaskPoint get;
    public TaskPoint target;
    public TaskPoint commit;

    public override TaskBase Go()
    {
        return this;
    }
    public override float Progress()
    {
        return 0f;
    }
}
public class TaskMain : TaskBase
{
    public List<TaskChild> childs;

    public override TaskBase Go()
    {
        return this;
    }
    public override float Progress()
    {
        return 0f;
    }
}

