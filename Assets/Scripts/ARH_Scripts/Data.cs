using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    [Button("Clear Data")]
    public void Clear()
    {
    }

    int NumberOfDifferentSpell = 0;


}