using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StoryType
{
    public class Attack : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.Attack;
        public string actor;
        public string target;
    }
}
