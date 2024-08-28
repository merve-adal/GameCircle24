using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SignChanger))]
public class SignChangerEditor : Editor
{

    Sign currentSign;
    Sign nextSign;

    public override void OnInspectorGUI()
    {
        SignChanger myTarget = (SignChanger)target;
        currentSign = myTarget.GetComponentInChildren<SignObject>().Sign;
        nextSign = (Sign)EditorGUILayout.EnumPopup("Sign:", currentSign);
        if (currentSign != nextSign) {
            myTarget.ChangeSign(nextSign);
        }
    }
}
