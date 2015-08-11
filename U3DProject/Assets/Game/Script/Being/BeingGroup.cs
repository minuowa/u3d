using UnityEngine;
using System.Collections;

public enum BeingGroup
{
    None,
    Player,  
    Npc, 
    Monster, 
}
public class GroupManager
{
    public static bool IsEnemy(BeingGroup t1, BeingGroup t2)
    {
        if (t1 == t2)
            return false;
        return true;
    }
}