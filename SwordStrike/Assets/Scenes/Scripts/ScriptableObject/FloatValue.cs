using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    [HideInInspector]
    public float RunTimeValue;

    public void OnAfterDeserialize()
    {
        RunTimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
