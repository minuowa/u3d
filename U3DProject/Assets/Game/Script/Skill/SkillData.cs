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
        public double distance;
        public int animition;

        public RangeData range;
        public EffectData effect;
        public DamageData damage;

        public static Data Get(int id)
        {
            return Mgr.instance.Get(id);
        }

        public void Execute(GameObject actor, GameObject victim)
        {
            Do(actor, victim);
            return;
            switch (range.type)
            {
                case RangeType.Single:
                    {
                        if (Vector3.Distance(actor.transform.position, victim.transform.position) <= distance)
                        {
                            Do(actor,victim);
                        }
                        else
                        {
                            MissionMgr mgr = actor.GetComponent<MissionMgr>();
                            if (mgr == null)
                                mgr = actor.AddComponent<MissionMgr>();

                            MissionFindPos find = new MissionFindPos();
                            find.target = victim.transform.position;
                            find.onwer = actor;
                            mgr.Add(find);

                            Executor executor = new Executor();
                            executor.skillid = id;
                            executor.actor = actor;
                            executor.victim = victim;

                            MissionSkill missskill = new MissionSkill();
                            missskill.executor = executor;
                            mgr.Add(missskill);
                        }
                    }
                    break;
                default:
                    {
                        Do(actor, null);
                    }
                    break;
            }
        }

        private void Do(GameObject actor, GameObject victim)
        {
            if(!actor)
                return;

            if (!string.IsNullOrEmpty(effect.name))
            {
                Effector effector = actor.AddComponent<Effector>();
                effector.data = effect;
            }

            Animator anim = actor.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetInteger(BeingAction.action, animition);
            }

            //BeingStat stat = actor.GetComponent<BeingStat>();
            DamageObject.Init(id, actor, victim);
        }

    }
}
