using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StoryType
{
    public class WarGuide : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.WarGuide;
        public string btnName = string.Empty;
        public Action onStart;
        public Action onEnd;

        void Start()
        {
            //GuideManager.Instance.ChangeTo(btnName, OnGuideStart, OnGuideEnd);
        }

        void OnGuideStart()
        {
        }
        void OnGuideEnd()
        {
            Next();
        }
    }
}

