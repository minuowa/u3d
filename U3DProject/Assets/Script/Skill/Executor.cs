﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Skill
{
    public class Executor
    {
        public Being actor;
        public Being victim;
        public int skillid;
        public void Execute()
        {
            Data data = Mgr.instance.Get(skillid);
            if (data!=null)
                data.Execute(actor, victim);
        }
    }
}