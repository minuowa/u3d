using UnityEngine;
using System.Collections;

public class Hero : Player
{
    public static Hero instance
    {
        get
        {
            return _instance;
        }
    }
    private static Hero _instance = null;

    protected HeroStat _heroStat;

    public Hero()
    {
        _instance = this;
        UnityEngine.Debug.Log("Hero()");
    }

    public override void Start()
    {
        base.Start();
        _heroStat = gameObject.AddComponent<HeroStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
