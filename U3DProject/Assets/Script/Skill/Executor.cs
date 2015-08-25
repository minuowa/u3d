using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Skill
{
    public class Executor:IMissionParam
    {
        public int skillid;
        public void Execute()
        {
            Data data = Mgr.instance.Get(skillid);
            if (data!=null)
                data.Execute(sender, receiver);
        }
    }
}
