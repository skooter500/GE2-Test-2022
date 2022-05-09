using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CornerCamera : MonoBehaviour
{
    public Transform cam;
    public Vector3 target;

    public Quaternion from;
    public float fromDistance;
    public float toDistance;
    public Quaternion to;

    public float angle = 45.0f;

    public float transitionTime = 2.0f;

    public float elapsed; 

    public float step = 25;
    public float min = 0;
    public float max = 200;
    public enum Transition {rotation, movement, time};

    public Transition transition = Transition.rotation; 

    NematodeSchool ns;

    public Utilities.EASE ease;

    float lastf = 0.5f;

    float oldTime = 2.5f;
    float newTime = 0;
    float oldShaderTime = 0; 
    float newShaderTime = 0; 

    public void StopStart(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        
        if (oldTime != 0)
        {
            Debug.Log("Starting");
            newTime = oldTime;
            oldTime = 0;
            newShaderTime = oldShaderTime;            
            oldShaderTime = 0;
            elapsed = 0.0f;
            transition = Transition.time;
        }
        else
        {
            Debug.Log("Stopping");            
            newTime = 0;
            oldTime = ns.ts;            
            newShaderTime = 0;
            oldShaderTime = ns.material.GetFloat("_TimeMultiplier");            
            elapsed = 0.0f;
            transition = Transition.time;
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    } 
    public void Light(InputAction.CallbackContext context)
    {
        
        float f = context.ReadValue<float>();    
        Debug.Log("CI: " + f);    
        ns.material.SetFloat("_CI", f);
    }
    public void AmbientLight(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();        
        Debug.Log("AI: " + f);
        RenderSettings.ambientLight = new Color(f,f,f,1);
    }

    public void RestartScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FeelerLength(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();        
        Debug.Log("Feeler Length: " + f);
        ns.feelerDepth = f;
    }


    public void TimeChanged(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        ns.ts = context.ReadValue<float>();
    }

    public void Forwards(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        fromDistance = -cam.transform.localPosition.z;
        toDistance = Mathf.Clamp(fromDistance - step, min, max);            
        elapsed = 0;
        transition = Transition.movement;
    }

    public void Backwards(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        fromDistance = -cam.transform.localPosition.z;
        toDistance = Mathf.Clamp(fromDistance + step, min, max);            
        elapsed = 0;
        transition = Transition.movement;
    }

    public void PitchClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.right) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void PitchCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.right) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void YawClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.up) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void YawCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.up) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void RollClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.forward) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void RollCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.forward) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }

    public void Radius(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        float f = context.ReadValue<float>();
        Debug.Log("Radius: " + f);
        ns.radius = f;
    }

    public void ColorRange(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        float f = context.ReadValue<float>() + 11;
        Debug.Log("Color Range: " + f);
        ns.material.SetFloat("_PositionScale", f);
    }

    public void ShaderTime(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Shader Time: " + f);
            ns.material.SetFloat("_TimeMultiplier", f);
        }
    }
            
    // Start is called before the first frame update
    void Start()
    {
        elapsed = transitionTime;
        fromDistance = -cam.transform.localPosition.z;
        toDistance = fromDistance;

        ns = FindObjectOfType<NematodeSchool>();
        oldShaderTime = ns.material.GetFloat("_TimeMultiplier");
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
                    , ease, 
                    Utilities.EASE.EASE_IN_OUT
                    );
            switch (transition)
            {
                case Transition.movement:
                {
                    float z = Mathf.Lerp(fromDistance, toDistance, t);
                    Vector3 camLocal = cam.transform.localPosition;
                    camLocal.z = -z;
                    cam.transform.localPosition = camLocal;
                    break;
                }
                case Transition.rotation:
                {
                    transform.rotation = Quaternion.Slerp(from, to, t);
                    break;
                }
                case Transition.time:
                {
                    ns.ts = Mathf.Lerp(oldTime, newTime, t);
                    ns.material.SetFloat("_TimeMultiplier",
                        Mathf.Lerp(oldShaderTime, newShaderTime, t) );
                    break;
                }
            }            
        }        
    }
}
