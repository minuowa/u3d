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
                    if (Hero.instance.mTarget)
                    {
                        Skill.Executor param = new Skill.Executor();
                        param.sender = Hero.instance;
                        param.skillID = 1001;
                        param.receiver = Hero.instance.mTarget;
                        Hero.instance.Do(ActionID.Skill, param);
                    }
                }
                break;
        }
    }
}
