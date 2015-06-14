using UnityEngine;
using System.Collections;

public class NameCard : MonoBehaviour
{
    public GUIStyle style;
    // Use this for initialization
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 25;
        style.clipping = TextClipping.Overflow;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI()
    {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        Vector3 offset = Vector3.zero;
        if (collider)
            offset.y = collider.height;
        if (Camera.main)
        {
            Animator animator = GetComponentInChildren<Animator>();
            Vector3 mypos;
            if (animator)
                mypos = animator.rootPosition;
            else
                mypos = transform.position;
            Vector3 pos = Camera.main.WorldToScreenPoint(mypos + offset);
            // shit....
            GUI.TextArea(new Rect(pos.x, Screen.height - pos.y, 0, 0), transform.name, style);
        }
    }
}
