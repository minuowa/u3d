using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Skill
{
    public enum BindType
    {
        Head0,
        Head1,
        HandLeft,
        HandRight,
        FootLeft,
        FootRight,
        WeaponLeft,
        WeaponRight,
    }
    public class EffectData
    {
        public string name;
        public BindType type;
    }
    public class Effector : MonoBehaviour
    {
        public EffectData data;
        void Start()
        {

        }
    }
}
