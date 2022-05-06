using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NematodeSchool : MonoBehaviour
{

float posScale = 0;
    public float endPosScale;
public float ts = 1.0f;
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
        material.SetFloat("_TimeMultiplier", timeScale);
        posScale = material.GetFloat(ps);
        endPosScale = posScale;
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

    float startPosScale;


    // Update is called once per frame
    void Update()
    {
        timeScale = ts;
        material.SetFloat("_TimeMultiplier", timeScale);        
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            radius = (radius == 5) ? 30 : 5;
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
            if (ts > 6f)
            {
                ts = 6f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            timeScale = 1.0f;
        }

        if (t < transitionTime)
        {
            t += Time.deltaTime;
            if (t > transitionTime)
            {
                t = transitionTime;
            }
            posScale = Utilities.Map(t, 0, transitionTime, startPosScale, endPosScale);
            //posScale = Utilities.Map2(t, 0, transitionTime, startPosScale, endPosScale, Utilities.EASE.EXPONENTIAL, Utilities.EASE.EASE_IN_OUT);
            material.SetFloat(ps, posScale);
        }

        float p = 0.02f;

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (endPosScale > minRange)
            {
                
                float posScale = material.GetFloat("_PositionScale");
                startPosScale = posScale;
                float jump = Utilities.Map(posScale, minRange, maxRange, 1, 50);
t = 0;
                endPosScale = endPosScale - jump;
                if (endPosScale < minRange)
                {
                    endPosScale = minRange;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {

            if (endPosScale < maxRange)
            {                
                float posScale = material.GetFloat("_PositionScale");
                startPosScale = posScale;
                float jump = Utilities.Map(posScale, minRange, maxRange, 1, 50);
                t = 0;
                endPosScale = endPosScale + jump;
                
                if (endPosScale > maxRange)
                {
                    endPosScale = maxRange;
                }
            }
        }
    }    
}
