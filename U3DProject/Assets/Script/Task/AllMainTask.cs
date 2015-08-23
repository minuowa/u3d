using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AllMainTask
{
    TaskBase cur;

    List<TaskMain> taskList;

    public AllMainTask()
    {
        taskList = new List<TaskMain>();
    }

    public TaskBase Next()
    {
        if (cur != null)
        {
            return cur.Go();
        }
        if (taskList.Count > 0)
        {
            cur = taskList[0];
        }
        return cur;
    }
    public void Stop()
    {

    }
    public void Go()
    {
        Next();
    }
}
