using UnityEngine;
using System.Collections;
[RequireComponent(typeof(StatNpc))]
[AddComponentMenu("RPG/Obj/Npc")]
public class Npc : Being
{

    protected StatNpc mStatNpc;
    // Use this for initialization
    public override void Start () {
        base.Start();
        mStatNpc = GetComponent<StatNpc>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
