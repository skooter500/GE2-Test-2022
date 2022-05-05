using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NematodeSchool : MonoBehaviour
{
    public GameObject prefab;

    [Range (1, 5000)]
    public int radius = 50;
    
    public int count = 10;

    public float speed = 2.0f;

    public float feelerDepth = 8;

    public float minRange = 38;
    public float maxRange = 200;


    public Material material;
    // Start is called before the first frame update

    string ps = "_PositionScale";
        

    void Start()
    {
        posScale = material.GetFloat(ps);
        targetPosScale = posScale;
        t = 1;
    }
    void Awake()
    {
        for(int i = 0 ; i < count ; i ++)
        {
            Vector3 pos = Random.insideUnitSphere * 5;
            pos = transform.TransformPoint(pos);
            Quaternion q = Quaternion.Euler(Random.Range(0.0f, 360), Random.Range(0.0f, 360), Random.Range(0.0f, 360));
            GameObject nematode = GameObject.Instantiate<GameObject>(prefab, pos, q);
            nematode.transform.parent = this.transform;
        }
    }

    public float transitionTime = 1.0f;
    float t;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            radius = (radius == 5) ? 60 : 5;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Time.timeScale -= Time.deltaTime * speed;
            if (Time.timeScale < 0 )
            {
                Time.timeScale = 0;
            }

        } 
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Time.timeScale += (Time.deltaTime * speed);
            if (Time.timeScale > 10f)
            {
                Time.timeScale = 10f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Time.timeScale = 1.0f;
        }

        float scaleFactor = 10;
        
        float posScale = material.GetFloat(ps); 

        if (t < transitionTime)
        {
            posScale 
            t += Time.deltaTime;
            if (t > transitionTime)
            {
                t = transitionTime;
            }
            posScale = Utilities.ma
        }
    
        material.SetFloat(ps, posScale);

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            if (targetPosScale > minRange)
            {
                t = 0;            
                targetPosScale -= scaleFactor;
                if (targetPosScale < minRange)
                {
                    targetPosScale = minRange;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {            
            targetPosScale += scaleFactor;
                
            if (targetPosScale > maxRange)
            {
                t = 0;
                targetPosScale = maxRange;
            }
        }
    	        
    }
    float posScale = 0;
    public float targetPosScale;
}
