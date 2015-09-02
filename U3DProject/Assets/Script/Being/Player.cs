using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StatPlayer))]
[DisallowMultipleComponent]
[AddComponentMenu("RPG/Obj/Player")]
public class Player : Being
{
    protected StatPlayer mStatPlayer;
    public override void Start () {
        base.Start();
        mStatPlayer = GetComponent<StatPlayer>();
    }

    // Update is called once per frame
    public override void Update () {
	
	}
}
