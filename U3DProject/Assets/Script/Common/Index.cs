using UnityEngine;
using System.Collections;

public class Index : MonoBehaviour {
    public static int InvalidIndex=-1;

    public int id = InvalidIndex;
    public int childId = InvalidIndex;

	public bool valid
    {
        get
        {
            return id != InvalidIndex;
        }
    }
}
