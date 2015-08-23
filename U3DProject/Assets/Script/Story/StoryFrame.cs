using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StoryType
{
    public class Clock
    {

        public delegate void OnTimeEnd(Clock c);

        void Begin(int waitTime, OnTimeEnd param2)
        {
        }
    }
    public class StoryFrame : MonoBehaviour
    {
        public int waitTime = 0;

        public void Next()
        {
            if (waitTime > 0)
            {
                //Clock c = ClockMgr.Instance.Require();
                //c.Begin(waitTime, OnNext);
            }
            else
            {
                OnNext(null);
            }
        }
        void OnNext(Clock c)
        {
            int cnt=transform.parent.childCount;
            int idx = 0;
            for (int i = 0; i < cnt; ++i)
            {
                Transform t = transform.parent.GetChild(i);
                if (t == transform)
                {
                    idx = i;
                    break;
                }
            }

            if (idx < cnt)
            {
                if (idx + 1 < cnt)
                {
                    Transform next = transform.parent.GetChild(idx + 1);
                    if (next)
                        next.gameObject.SetActive(true);
                    else
                        GameObject.Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
