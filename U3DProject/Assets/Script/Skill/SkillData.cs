using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Skill
{
    public class Data
    {
        public int id;
        public float distance;
        public int animition;

        public RangeData range;
        public EffectData effect;
        public DamageData damage;

        public static Data Get(int id)
        {
            return Mgr.instance.Get(id);
        }

        public bool IsInRange(Vector3 center, Vector3 pos)
        {
            if (range.type == RangeType.Single)
                return Vector3.Distance(center, pos) <= distance;
            return true;
        }

        public void Execute(Being actor, Being victim)
        {
            Do(actor, victim, 0);

            switch (range.type)
            {
                case RangeType.Single:
                    {

                    }
                    break;
                default:
                    {
                        Do(actor, null, 0);
                    }
                    break;
            }
        }

        private void Do(Being actor, Being victim, int missionid)
        {
            if (!actor)
                return;

            if (!string.IsNullOrEmpty(effect.name))
            {
                Effector effector = actor.gameObject.AddComponent<Effector>();
                effector.data = effect;
            }

            Animator anim = actor.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetInteger(BeingAnimation.action, animition);
            }

            //BeingStat stat = actor.GetComponent<BeingStat>();
            DamageObject.Init(id, actor, victim, missionid);
        }

    }
}
