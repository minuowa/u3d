using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace StoryType
{
    public class Create : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.Create;
        public GameObject prefab;
        public string actor;
    }
}
