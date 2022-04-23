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
            Vector3 pos = new Vector3(0, 0, i);
            pos = transform.TransformPoint(pos);
            seg.transform.position = pos;
            seg.transform.rotation = this.transform.rotation;
            seg.GetComponent<Renderer>().material = material;
        }
        float s = 0.1f;
        switch (gender)
        {
            case Gender.Male:
                GameObject penis = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                penis.transform.Rotate(90, 0, 0);
                penis.transform.localScale = new Vector3(0.1f, 1, 0.1f);
                Vector3 pos = new Vector3(0,0,length + 1);
                pos = transform.TransformPoint(pos);
                penis.transform.position = pos;
                break;
            case Gender.Female:
                GameObject vagina = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                vagina.transform.localScale = new Vector3(s, s, s);
                Vector3 p = new Vector3(0, 0.5f + (s * 0.5f), length);
                vagina.transform.position = p;
                break;

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
