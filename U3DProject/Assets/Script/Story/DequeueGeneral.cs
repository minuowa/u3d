using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StoryType
{
    public class DequeueGeneral : StoryFrame
    {
        public static StoryIndex storyType = StoryIndex.DequeueGeneral;
        public int idx = 0;

        void Start()
        {
            //WarSceneController.Instance.DequeueGeneral(idx);
            Next();
        }
    }
}
