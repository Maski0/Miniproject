using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looktowardsmouse : MonoBehaviour
{
    public Camera cam;
    public float maxlenght;

    Ray raymouse;
    Vector3 pos;
    Vector3 dir;
    Quaternion rotation;

    void Update()
    {
        if (cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            raymouse = cam.ScreenPointToRay(mousePos);
            if(Physics.Raycast(raymouse.origin, raymouse.direction, out hit))
            {
                rotateTomousePoint(gameObject, hit.point);
                //Debug.Log(hit.transform.name);
            }
            else
            {
                var pos = raymouse.GetPoint(maxlenght);
                //Debug.Log(pos);
                rotateTomousePoint(gameObject, pos);
            }
        }
        else
            Debug.Log("There is no Cam");
        
    }

    void rotateTomousePoint(GameObject obj, Vector3 destination)
    {
        dir = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(dir);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation,0);
    }
    public Quaternion getrotation()
    {
        return rotation;
    }
}
