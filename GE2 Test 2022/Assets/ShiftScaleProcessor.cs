using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif


public class ShiftScaleProcessor : InputProcessor<float>
{
    #if UNITY_EDITOR
    static ShiftScaleProcessor()
    {
        Initialize();
    }
    #endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<ShiftScaleProcessor>();
    }

    [Tooltip("Shift")]
    public float shift = 0;

    [Tooltip("Scale")]
    public float scale = 1;

    public override float Process(float value, InputControl control)
    {
        Debug.Log("Value:" + value);
        return (value + shift) * scale;
    }
}
