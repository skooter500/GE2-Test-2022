using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
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
    public enum Transition {rotation, movement, time, shaderTime};

    public Transition transition = Transition.rotation; 

    NematodeSchool ns;

    public Utilities.EASE ease;

    float lastf = 0.5f;

    float oldTime = 2.5f;
    float newTime = 0;
    float oldShaderTime = 0; 
    float newShaderTime = 0; 

    float oldTransitionTime = 0;

    bool stopped = true;

    public Gradient gradient;


    public void StopStart(InputAction.CallbackContext context)
    {

        if (elapsed != transitionTime)
        {
            return;
        }
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        
        if (stopped)
        {
            Debug.Log("Starting");
            newTime = CornerCamera.timeScale;;
            oldTime = 0;            
            newShaderTime = oldShaderTime;            
            oldShaderTime = 0;
            elapsed = 0.0f;
            transition = Transition.time;
            
            //Invoke("ShaderTimeTransition", transitionTime);
        }
        else
        {
            Debug.Log("Stopping");            
            newTime = 0;
            oldTime = ns.ts;            
            newShaderTime = 0;
            oldShaderTime = ns.material.GetFloat("_TimeMultiplier");       
            //ns.material.SetFloat("_TimeMultiplier", 0);
            elapsed = 0.0f;
            transition = Transition.time;
            oldTransitionTime = transitionTime;

            //Invoke("ShaderTimeTransition", transitionTime);
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    } 
    public void Light(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();    
        Debug.Log("Center Light: " + f);    
        ns.material.SetFloat("_CI", f);
    }

    private static bool shouldIgnore = false;
    private bool ShouldIgnore(InputAction.CallbackContext context)
    {
        bool b = Mathf.Abs(Time.time - (float) context.time) > 1000;
        if (b)
        {
            Debug.Log("Ignoring: " + context);
            shouldIgnore = true;
        }
        return b || shouldIgnore;
    }

    public void Alpha(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();    
        Debug.Log("Alpha: " + f);    
        ns.material.SetFloat("_Alpha", f);
    }
    public void AmbientLight(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        
        float f = context.ReadValue<float>();        
        Debug.Log("Ambient Light: " + f);
        RenderSettings.ambientLight = new Color(f,f,f,1);
    }

    public void RestartScene(InputAction.CallbackContext context)
    {
        CornerCamera.timeScale = ns.ts;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FeelerLength(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        
        float f = context.ReadValue<float>();        
        Debug.Log("Front Feeler Length: " + f);
        ns.feelerDepth = f;
    }

    public void SideFeelerLength(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        
        Debug.Log("Side Feeler Length: " + f);
        ns.sideFeelerDepth = f;
    }

    private Bloom bloom;
    public PostProcessVolume volume;

    public void Bloom(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        
        Debug.Log("Bloom: " + f); 
        
        bloom.intensity.Override(f);
    }

    public void ColorWidth(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        
        Debug.Log("Color Width : " + f);
        ns.material.SetFloat("_ColorWidth", f);
        
    }


    public void ColorStart(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        else
        {
            Debug.Log("Not Ignoring: " + context);
        }
        float f = context.ReadValue<float>() - 0.5f;        
        f *= 2.0f;
        Debug.Log("Color Start : " + f);
        ns.material.SetFloat("_ColorStart", f);
        
    }
    public void ColorEnd(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() +- 0.5f;       
        f *= 2.0f; 
        Debug.Log("Color End : " + f);
        ns.material.SetFloat("_ColorEnd", f);
        
    }



    public void ColorShift(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        

        
        Debug.Log("Color Shift: " + f);
        ns.material.SetFloat("_ColorShift", f);

    }



    public void TimeChanged(InputAction.CallbackContext context)
    {

        if (ShouldIgnore(context))
        {
            return;
        }        
        
        Debug.Log("Time Changed: " + context.ReadValue<float>() + "stopped: " + stopped);
        tTimeChanged = context.ReadValue<float>();
        if (! stopped)
        {
            ns.ts = tTimeChanged;
            newTime = tTimeChanged;
        }   
        else
        {
            newTime = tTimeChanged;
            CornerCamera.timeScale = newTime;
        }     
    }
    float tTimeChanged = 0; 

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
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();
        Debug.Log("Radius: " + f);
        ns.radius = f;
    }

    public void ColorRange(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() + 1;
        Debug.Log("Range: " + f);
        ns.material.SetFloat("_PositionScale", f);
    }

    public void ShaderTime(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();
        Debug.Log("Shader Time: " + f);
        ns.material.SetFloat("_TimeMultiplier", f);
    }

    private static float timeScale = 2.5f;

    void Awake()
    {
        ns = FindObjectOfType<NematodeSchool>();
        ns.ts = 0;
        oldShaderTime = 1;
        oldTime = CornerCamera.timeScale;                
    }
            
    // Start is called before the first frame update
    void Start()
    {
        float v = 100.0f;
        RenderSettings.ambientLight = new Color(v,v,v,1);
        elapsed = transitionTime;
        fromDistance = -cam.transform.localPosition.z;
        toDistance = fromDistance;

        bloom = volume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();

        oldTime = CornerCamera.timeScale;
        
        newTime = 0;

        
        //ns.material.SetFloat("_TimeMultiplier", 0);
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
                    Debug.Log("New Time: " + newTime);
                    ns.ts = Mathf.Lerp(oldTime, newTime, t);         
                    //float timeM = Mathf.Lerp(oldShaderTime, newShaderTime, t);                     
                    //ns.material.SetFloat("_TimeMultiplier", timeM);      
                    if (elapsed == transitionTime)
                    {
                        stopped = ! stopped;
                    }   
                    break;                    
                }                
            }            
        }        
    }
}
