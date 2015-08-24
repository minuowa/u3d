using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class NameCard : MonoBehaviour
{
    public GUIStyle style;
    public Texture2D blood;

    Vector3 mPos;
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
            mPos = Camera.main.WorldToScreenPoint(transform.position + offset);
            // shit....
            Rect rc = new Rect(mPos.x, Screen.height - mPos.y, 0, 0);
            GUI.TextArea(rc, transform.name, style);
            int w = 100;
            int h = 20;
            rc.x = mPos.x - w / 2;
            rc.y = Screen.height - mPos.y - 30;
            rc.width = w;
            rc.height = h;
            if (blood)
                GUI.DrawTexture(rc, blood);
        }
    }
    public  void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mPos != null)
            Gizmos.DrawWireSphere(mPos, 5);
    }
}
