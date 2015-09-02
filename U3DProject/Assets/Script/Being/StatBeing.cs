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
public class StatBeing : MonoBehaviour
{
    public BeingType beingType;
    public BeingGroup group = BeingGroup.None;
    public int maxHp = 100;
    public int hp = 0;
    public int modelID = 0;
    public string ai = string.Empty;
}
