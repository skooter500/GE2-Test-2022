using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCamera : MonoBehaviour
{
    public Transform cam;
    public Vector3 target;

    public float distance  = 20;

    public Quaternion from;
    private float fromDistance;
    private float toDistance;
    public Quaternion to;

    public float angle = 45.0f;

    public float transitionTime = 2.0f;

    public float elapsed; 

    public float moveDistance;
    public float maxDistance;
    public enum Transition {rotation, movement};

    public Transition transition = Transition.rotation; 
        
    // Start is called before the first frame update
    void Start()
    {
        elapsed = transitionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            if (elapsed > transitionTime)
            {
                elapsed = transitionTime;
            }

            float t = Utilities.Map2(elapsed, 0, transitionTime, 0, 1
                    , Utilities.EASE.QUADRATIC, 
                    Utilities.EASE.EASE_IN_OUT
                    );
            switch (transition)
            {
                case Transition.movement:
                {
                    float z = Mathf.Lerp(fromDistance, toDistance, t);
                    Vector3 camLocal = cam.transform.localPosition;
                    camLocal.z = z;
                    cam.transform.localPosition = camLocal;
                    break;
                }
                case Transition.rotation:
                {
                    transform.rotation = Quaternion.Slerp(from, to, t);
                    break;
                }
            }            
        }
        
        float threshold = 0.5f;

        
        if (Input.GetAxis("RHorizontal") > threshold && elapsed == transitionTime)
        {
            Debug.Log("right");
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.up) * transform.rotation;
            Debug.Log(transform.up);
            Debug.Log(to.eulerAngles);
            elapsed = 0;
            transition = Transition.rotation;
        }

        if (Input.GetAxis("RHorizontal") < - threshold && elapsed == transitionTime)
        {
            Debug.Log("left");
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.up) * transform.rotation;
            elapsed = 0;
            transition = Transition.rotation;
        }
        
        if (Input.GetAxis("RVertical") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Up");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.right) * transform.rotation;
            elapsed = 0;
            transition = Transition.rotation;
        }

        if (Input.GetAxis("RVertical") < -threshold && elapsed == transitionTime)
        {
            Debug.Log("down");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.right) * transform.rotation;
            elapsed = 0;
            transition = Transition.rotation;
        }        

        if (Input.GetAxis("Horizontal") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Rright");
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.forward) * transform.rotation;
            Debug.Log(transform.up);
            Debug.Log(to.eulerAngles);
            elapsed = 0;
            transition = Transition.rotation;
        }

        if (Input.GetAxis("Horizontal") < - threshold && elapsed == transitionTime)
        {
            Debug.Log("Rleft");
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.forward) * transform.rotation;
            elapsed = 0;
            transition = Transition.rotation;
        }
        
        
        if (Input.GetAxis("Vertical") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Up");
            
            fromDistance = cam.transform.localPosition.z;
            toDistance = fromDistance + moveDistance;
            elapsed = 0;
            transition = Transition.movement;
        }

        if (Input.GetAxis("Vertical") < -threshold && elapsed == transitionTime)
        {
            Debug.Log("down");
            
            fromDistance = cam.transform.localPosition.z;
            toDistance = fromDistance - moveDistance;
            elapsed = 0;
            transition = Transition.movement;
        }
        */
        

        //this.transform.rotation = to;
    }
}
