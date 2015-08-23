using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class Range
    {
        public static Range Get(RangeType type)
        {
            Range range = new Range();
            return range;
        }
    }
}
