using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NematodeSchool : MonoBehaviour
{
    public GameObject prefab;

    [Range (1, 5000)]
    public int radius = 50;
    
    public int count = 10;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0 ; i < count ; i ++)
        {
            Vector3 pos = Random.insideUnitSphere * radius;
            pos = transform.TransformPoint(pos);
            Quaternion q = Quaternion.AngleAxis(Random.Range(0.0f, 360), Vector3.up);
            GameObject nematode = GameObject.Instantiate<GameObject>(prefab, pos, q);
            nematode.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            radius = (radius == 5) ? 75 : 5;
        }
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Time.timeScale -= (Time.deltaTime);
            if (Time.timeScale < 0 )
            {
                Time.timeScale = 0;
            }

        }
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Time.timeScale += (Time.deltaTime);
            if (Time.timeScale > 5)
            {
                Time.timeScale = 5;
            }
        }
    }
}
