using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class HeroClick : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UICamera.hoveredObject != null && UICamera.hoveredObject.layer == 5)
            {
                if(UICamera.hoveredObject.tag!="UIRoot")
                    return;
            }
            if (Camera.main == null)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObj=hit.transform.gameObject;
                var com = hitObj.GetComponent<Terrain>();
                if (com != null)
                {
                    GroundMove move = Hero.instance.TryGetComponent<GroundMove>();
                    move.target = hit.point;
                    return;
                }
                Being being=hitObj.GetComponent<Being>();
                if (being != null)
                {
                    if (hitObj != Hero.instance.gameObject)
                    {
                        if (Hero.instance.target != null)
                            Hero.instance.target.Unselect();
                        Hero.instance.target = being;
                        being.Select();
                        return;
                    }
                }
            }
        }
    }
}
