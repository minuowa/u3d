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
                //if(UICamera.hoveredObject.tag!="UIRoot")
                //    return;
            }
            if (Camera.main == null)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObj=hit.transform.gameObject;
                if (hitObj.layer==(int)GameLayer.Terrain)
                {
                    GroundMoveParam moveParam = new GroundMoveParam();
                    moveParam.rawpos = hit.point;
                    Hero.instance.Do(ActionID.MoveTo, moveParam);
                    return;
                }
                Being being=hitObj.GetComponent<Being>();
                if (being != null)
                {
                    if (hitObj != Hero.instance.gameObject)
                    {
                        SelectParam sleparam = new SelectParam();
                        sleparam.receiver = being;
                        Hero.instance.Do(ActionID.SelectTarget, sleparam);

                        SkillParam param=new SkillParam();
                        param.skillID=1001;
                        param.receiver = being;
                        Hero.instance.Do(ActionID.Skill,param);
                        return;
                    }
                }
            }
        }
    }
}
