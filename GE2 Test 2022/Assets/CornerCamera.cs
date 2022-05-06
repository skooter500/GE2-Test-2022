using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCamera : MonoBehaviour
{
    public Transform cam;
    public Vector3 target;

    public float distance  = 20;

    public Quaternion from;
    public Quaternion to;

    public float angle = 45.0f;

    public float transitionTime = 2.0f;

    public float elapsed; 
        
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
            transform.rotation = Quaternion.Slerp(from, to, t);
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
        }

        if (Input.GetAxis("RHorizontal") < - threshold && elapsed == transitionTime)
        {
            Debug.Log("left");
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.up) * transform.rotation;
            elapsed = 0;
        }
        
        if (Input.GetAxis("RVertical") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Up");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.right) * transform.rotation;
            elapsed = 0;
        }

        if (Input.GetAxis("RVertical") < -threshold && elapsed == transitionTime)
        {
            Debug.Log("down");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.right) * transform.rotation;
            elapsed = 0;
        }        

        if (Input.GetAxis("Horizontal") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Rright");
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.forward) * transform.rotation;
            Debug.Log(transform.up);
            Debug.Log(to.eulerAngles);
            elapsed = 0;
        }

        if (Input.GetAxis("Horizontal") < - threshold && elapsed == transitionTime)
        {
            Debug.Log("Rleft");
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.forward) * transform.rotation;
            elapsed = 0;
        }
        
        /*
        if (Input.GetAxis("RVertical") > threshold && elapsed == transitionTime)
        {
            Debug.Log("Up");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(angle, transform.right) * transform.rotation;
            elapsed = 0;
        }

        if (Input.GetAxis("RVertical") < -threshold && elapsed == transitionTime)
        {
            Debug.Log("down");
            
            from = transform.rotation;
            to = Quaternion.AngleAxis(-angle, transform.right) * transform.rotation;
            elapsed = 0;
        }
        */
        

        //this.transform.rotation = to;
    }
}
