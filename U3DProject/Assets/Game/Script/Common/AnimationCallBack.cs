using UnityEngine;
using System.Collections;

public class AnimationCallBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnJumpEnd()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger(BeingAction.action, BeingAction.Idle0);
    }
}
