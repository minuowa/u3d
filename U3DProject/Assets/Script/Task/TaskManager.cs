using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public AllMainTask mainTask;

    void Start()
    {
        if (mainTask != null)
            mainTask.Go();
    }
}
