using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StatPlayer))]
[AddComponentMenu("RPG/Obj/Player")]
public class Player : Being
{
    protected StatPlayer mStatPlayer;
    public override void Start () {
        base.Start();
        mStatPlayer = GetComponent<StatPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
