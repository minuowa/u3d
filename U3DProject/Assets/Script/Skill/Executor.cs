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
            Config.SkillData data = Config.SkillData.Get(skillid);
            if (data != null)
                data.Execute(sender, receiver);
        }
    }
}
