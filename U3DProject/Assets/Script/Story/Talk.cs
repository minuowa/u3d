using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace StoryType
{
    public class Talk : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.Talk;
        public string actor;
        public string content;
    }
}