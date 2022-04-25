using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nematode : MonoBehaviour
{
    public int length = 5;

    public Material material;

    void Awake()
    {
        GameObject head = null;
        for(int i = 0 ; i < length ; i ++)
        {            
            GameObject seg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
         
            if (i == 0)
            {
                head = seg;
            }

            Vector3 pos = new Vector3(0, 0, -i);
            pos = transform.TransformPoint(pos);
            seg.transform.position = pos;
            float range = 0.2f; 
            float s = Mathf.Sin(Utilities.Map(i, 0, length - 1, range, Mathf.PI - range)) * 2;
            seg.transform.localScale = new Vector3(s, s, 1);
            seg.transform.rotation = this.transform.rotation;
            seg.transform.parent = this.transform;
            seg.GetComponent<Renderer>().material = material;
            seg.GetComponent<Renderer>().material.color = Color.HSVToRGB(i / (float) length, 1.0f, 1.0f); 
        }

        head.AddComponent<Boid>();
        head.AddComponent<ObstacleAvoidance>().mask = gameObject.layer;
        Constrain c = head.AddComponent<Constrain>();
        c.weight = 3;
        c.radius = 20;

        head.AddComponent<NoiseWander>().weight = 6;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
