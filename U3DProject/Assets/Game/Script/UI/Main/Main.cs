using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnToggle(UIButton toggle)
    {
        int id = toggle.index;
        switch (id)
        {
            case 0:
                {
                    if (Hero.instance.target)
                    {
                        Skill.Executor skillExecutor = new Skill.Executor();
                        skillExecutor.skillid = 1001;
                        skillExecutor.actor = Hero.instance.gameObject;
                        skillExecutor.victim = Hero.instance.target.gameObject;
                        skillExecutor.Execute();
                    }
                }
                break;
        }
    }
}
