using UnityEngine;
using System.Collections;
using Skill;

public class AnimationCallBack : MonoBehaviour {

	// Use this for initialization
    public GameObject bullet;
    public GameObject normalAttackEffect;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnJumpEnd()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger(BeingAnimation.action, BeingAnimation.Idle0);
    }
    public void OnArcher()
    {
        if (bullet)
        {
            DamageObject damage = bullet.GetComponent<DamageObject>();
            if (damage!=null)
            {
                damage.Shot();
            }
        }
    }
}
