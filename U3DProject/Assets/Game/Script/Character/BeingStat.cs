using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public enum BeingType
{
    None = 0,
    Player = 1,
    Hero = Player | (2),
    Npc = 4,
    Monster = 8 | Npc,
}
public class BeingStat : MonoBehaviour
{
    public int globalID;
    public BeingType beingType;
}
