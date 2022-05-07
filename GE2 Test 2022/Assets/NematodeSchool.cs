using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NematodeSchool : MonoBehaviour
{

    public float sv1, ev1;
float posScale = 0;
    public float endValue;
public float ts = 1.0f;
public float shaderTs = 1.0f;
    public static float timeScale = 1.0f;  
    public GameObject prefab;

    [Range(1, 5000)]
    public int radius = 50;

    public int count = 10;

    public float speed = 2.0f;

    public float feelerDepth = 8;

    public float minRange = 38;
    public float maxRange = 200;

    float t = 0;

    public Material material;
    // Start is called before the first frame update

    string ps = "_PositionScale";


    void Start()
    {
        material.SetFloat("_TimeMultiplier", shaderTs);
        posScale = material.GetFloat(ps);
        endValue = posScale;
        timeScale = ts;
        t = transitionTime;
    }
    void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 5;
            pos = transform.TransformPoint(pos);
            Quaternion q = Quaternion.Euler(Random.Range(0.0f, 360), Random.Range(0.0f, 360), Random.Range(0.0f, 360));
            GameObject nematode = GameObject.Instantiate<GameObject>(prefab, pos, q);
            nematode.transform.parent = this.transform;
        }
    }

    public float transitionTime = 2.0f;

    public float maxJump = 10;

    float startValue;

    int[] rads = {1, 30, 60, 90, 120};
    int iR = 0;

    bool lastClicked = false;

    public enum Transition {scale, speed};

    public Transition transition = Transition.scale; 

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
            switch(transition)
            {
                case Transition.scale:
                {
                    float y = Utilities.Map2(t, 0, transitionTime, startValue, endValue, Utilities.EASE.CUBIC, Utilities.EASE.EASE_IN_OUT);
                    posScale = y;
                    material.SetFloat(ps, y);
                    break;
                }
                case Transition.speed:
                {
                    float y = Utilities.Map2(t, 0, transitionTime, sv1, ev1, Utilities.EASE.CUBIC, Utilities.EASE.EASE_IN_OUT);
                    shaderTs = y;
                    material.SetFloat("_TimeMultiplier", y);
                    break;
                }
            }            
        }

        float p = 0.02f;

        timeScale = ts;      
        /*  
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            radius = rads[iR];
            iR = (iR + 1) % rads.Length;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            ts -= Time.deltaTime * speed;
            if (ts < 0)
            {
                ts = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            
            ts += (Time.deltaTime * speed);
            if (ts > 5f)
            {
                ts = 5f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            timeScale = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("here");
            if (endValue > minRange)
            {
                transition = Transition.scale;
                float posScale = material.GetFloat("_PositionScale");
                startValue = posScale;
                float jump = Utilities.Map(posScale, minRange, maxRange, 1, 50);
t = 0;
                endValue = endValue - jump;
                if (endValue < minRange)
                {
                    endValue = minRange;
                }
            }
        }

        float threshold = 0.5f;

        Debug.Log(Input.GetAxis("DPadX"));

        float x = Input.GetAxis("DPadX");

        if (Mathf.Abs(x) < threshold)
        {
            lastClicked = false;
        }

        if (x < (-1.0f + threshold) && ! lastClicked)
        {
            sv1 = shaderTs;
            ev1 = shaderTs - (Time.deltaTime * speed * 5);
            if (ev1 < 0)
            {
                ev1 = 0;
            }
            lastClicked = true;
            //transition = Transition.speed;
            //t = 0.0f;
            shaderTs = ev1;
            material.SetFloat("_TimeMultiplier", shaderTs);
        
        }

        if (x > (1.0f - threshold) && ! lastClicked)
        {
            sv1 = shaderTs;
            ev1 = shaderTs + (Time.deltaTime * speed * 5);
            if (ev1 > 100)
            {
                ev1 = 100;
            }
            shaderTs = ev1;
            material.SetFloat("_TimeMultiplier", shaderTs);
            lastClicked = true;

        
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {

            if (endValue < maxRange)
            {                
                float posScale = material.GetFloat("_PositionScale");
                startValue = posScale;
                float jump = Utilities.Map(posScale, minRange, maxRange, 1, 50);
                t = 0;
                endValue = endValue + jump;
                
                if (endValue > maxRange)
                {
                    endValue = maxRange;
                }
                transition = Transition.scale;
            }
        }
        */
    }    
}
