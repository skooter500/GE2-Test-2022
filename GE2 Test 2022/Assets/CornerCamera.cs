using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCamera : MonoBehaviour
{
    public Transform cam;
    public float baseHeight = 50;
    public Vector3 target;

    public float height = 0;

    float baseLength;

    float targetNoiseScale;
    public float corner  = 20;
        
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        target = new Vector3(0, 0, - corner);
        transform.position = target;
        baseLength = target.magnitude;
        t = transitionTime;
        original = target;
   }

   public float transitionTime = 1.0f;
   Vector3 original;

   public float angle = 0;

public float t = 0;
int dir = 0;

    // Update is called once per frame
    void Update()
    {
        
        if (t < transitionTime)
        {
            t += Time.deltaTime;
            if (t > transitionTime)
            {
                t = transitionTime;
            }
            float a = Utilities.Map2(t, 0, transitionTime, oldAngle, angle, Utilities.EASE.QUADRATIC, Utilities.EASE.EASE_IN_OUT);
            Quaternion q = (dir == 0) ? Quaternion.AngleAxis(a, transform.up) : Quaternion.AngleAxis(a, transform.right);
        cam.transform.position = q * original;    
        cam.transform.LookAt(Vector3.zero);    
        }
        Vector3 toC = - transform.position;
        Vector3 axis = Quaternion.AngleAxis(10, transform.right) * transform.up;
        

        //noiseCube.noiseScale = Mathf.Lerp(noiseCube.noiseScale, targetNoiseScale, Time.deltaTime * 0.1f);


float threshold = 0.5f;
float d = 20;
        if (Input.GetAxis("Horizontal") > threshold && t == transitionTime)
        {
            t = 0;
            dir = 0;
            oldAngle = angle;
            angle += d;
        }
        if (Input.GetAxis("Horizontal") < -threshold && t == transitionTime)
        {
            t = 0;
            dir = 0;
            oldAngle = angle;
            angle -= d;
        }

        if (Input.GetAxis("Vertical") > threshold && t == transitionTime)
        {
            t = 0;
            dir = 1;
            oldAngle = angle;
            angle += d;
        }
        if (Input.GetAxis("Vertical") < -threshold && t == transitionTime)
        {
            t = 0;
            dir = 1;
            oldAngle = angle;
            angle -= d;
        }
        
    }
    float oldAngle = 0;
}
