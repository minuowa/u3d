using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Information : MonoBehaviour
{
    public static Information Instance
    {
        get
        {
            return MS<Information>.Instance;
        }
    }

}
