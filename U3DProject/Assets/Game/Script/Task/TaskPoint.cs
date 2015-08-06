using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum TaskPointType
{
    Npc,
    UI,
}
public class TaskPoint
{
    public TaskPointType type = TaskPointType.Npc;
    public int uiID;
    public int uiparam;
    public int mapid;
    public int npcid;
    public float x;
    public float z;
}
