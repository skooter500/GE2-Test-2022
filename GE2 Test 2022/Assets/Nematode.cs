using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nematode : MonoBehaviour
{
    public int length = 5;
    public bool legs = true;
    public bool eyes = true;

    public enum Gender {Male, Female, Hermaphrodide, None};

    public Material material;

    Gender gender;


    void Awake()
    {
        for(int i = 0 ; i < length ; i ++)
        {
            GameObject seg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Vector3 pos = new Vector3(0, 0, -i);
            pos = transform.TransformPoint(pos);
            seg.transform.position = pos;
            seg.transform.rotation = this.transform.rotation;
            seg.GetComponent<Renderer>().material = material;
            seg.transform.parent = this.transform;
        }
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
