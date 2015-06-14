using UnityEngine;
using System.Collections;

public class Player : Being {

    protected PlayerStat _playerStat;
	// Use this for initialization
	public override void Start () {
        base.Start();
        _playerStat = gameObject.AddComponent<PlayerStat>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
