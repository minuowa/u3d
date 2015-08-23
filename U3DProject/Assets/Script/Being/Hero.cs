using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HeroClick))]
[RequireComponent(typeof(D2HeroCamera))]
[RequireComponent(typeof(AnimationCallBack))]
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("RPG/Obj/Hero")]
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

    protected StatHero mStatHero;

    public Hero()
    {
    }

    public override void Start()
    {
        _instance = this;
        base.Start();
        mStatHero = gameObject.GetComponent<StatHero>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
