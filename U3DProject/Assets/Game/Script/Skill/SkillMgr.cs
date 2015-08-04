using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Skill
{
    public class Mgr
    {
        public static Mgr instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance=new Mgr();
                }
                return _instance;
            }
        }
        static Mgr _instance=null;
        public Skill.Data Get(int id)
        {
            if (_skills.ContainsKey(id))
                return _skills[id];
            return null;
        }
        private static Dictionary<int, Data> _skills;

        public Mgr()
        {
            InitSkills();
        }

        bool InitSkills()
        {
            _skills = new Dictionary<int, Data>();
            Data data = new Data();
            data.id = 1001;
            data.animition = "jump_pose";
            data.damage = new DamageData();
            data.damage.prefab = "Prefabs/GameObject/bullet";
            data.distance = 5f;
            data.effect = new EffectData();
            data.range = new RangeData();
            data.range.ridus = 3;
            data.range.type = RangeType.Single;
            _skills.Add(data.id, data);
            return true;
        }
    }

}

