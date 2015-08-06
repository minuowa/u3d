using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace StoryType
{
    public class Move : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.Move;
        public string actor;
        public Vector3 target;
    }
}