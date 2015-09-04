using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Skill
{
    public class Executor:IMissionParam
    {
        public int skillID;
        public int step;
        public Config.SkillData data
        {
            get
            {
                return Config.SkillData.Get(skillID);
            }
        }
        public void Execute(int missionid)
        {
            if (data != null)
                data.Execute(sender, receiver, missionid);
        }
        public override Mission Create()
        {
            var mission = new MissionSkill();
            mission.param = this;
            return mission;
        }
    }
}
