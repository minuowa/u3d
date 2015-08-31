using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Skill
{
    public enum RangeType
    {
        Single,
        Quad,
        Circle,
        Sector,
    }
    public class RangeData
    {
        public RangeType type;
        public float ridus;
    }
}
