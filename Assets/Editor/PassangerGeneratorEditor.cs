using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PassengerGenerator))]
public class PassangerGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PassengerGenerator myTarget = (PassengerGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            myTarget.Generate();
        }
        
    }
}
