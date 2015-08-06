using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StoryType
{
    public class PlayAdviserSkill : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.PlayAdviserSkill;

        void Start()
        {
            //WarSceneController.Instance.DequeueGeneral(3);
            Next();
        }
    }
}
