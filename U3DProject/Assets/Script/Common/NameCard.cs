using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class NameCard : MonoBehaviour
{
    public GUIStyle style;
    public Texture blood;
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
        CapsuleCollider collider = GetComponentInParent<CapsuleCollider>();
        Vector3 offset = Vector3.zero;
        if (collider)
            offset.y = collider.height;
        if (Camera.main)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
            // shit....
            Rect rc=new Rect(pos.x, Screen.height - pos.y, 0, 0);
            GUI.TextArea(rc, transform.name, style);
            if (blood)
                GUI.DrawTexture(rc, blood);
        }
    }
}
