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
