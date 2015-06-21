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

        public void Execute(GameObject actor, MissionSkill mission, GameObject victim)
        {
            switch (range.type)
            {
                case RangeType.Single:
                    {
                        if (mission)
                        {
                            Do(actor, victim, mission.id);
                        }
                        else
                        {
                            MissionMgr mgr = actor.GetComponent<MissionMgr>();
                            if (mgr == null)
                                mgr = actor.AddComponent<MissionMgr>();

                            GroundMove move = actor.gameObject.GetComponent<GroundMove>();
                            if (move == null)
                                move = actor.gameObject.AddComponent<GroundMove>();

                            move.target = victim.gameObject.transform.position;
                            move.miniDistance = distance;
                            move.Work();

                            Executor executor = new Executor();
                            executor.skillid = id;
                            executor.actor = actor;
                            executor.victim = victim;
                            MissionSkill missskill = actor.gameObject.GetComponent<MissionSkill>();
                            if (missskill == null)
                                missskill = actor.gameObject.AddComponent<MissionSkill>();
                            executor.mission = missskill;
                            missskill.executor = executor;
                            missskill.Work();
                        }
                    }
                    break;
                default:
                    {
                        Do(actor, null,0);
                    }
                    break;
            }
        }

        private void Do(GameObject actor, GameObject victim, int missionid)
        {
            if (!actor)
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
            DamageObject.Init(id, actor, victim,missionid);
        }

    }
}
