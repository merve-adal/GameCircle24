using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FinishLines", menuName = "ScriptableObjects/FinishLines")]
public class FinishLinesScriptableObject : ScriptableObject
{
    [SerializeField] private Vector3 upFinishLine;
    [SerializeField] private Vector3 rightFinishLine;
    [SerializeField] private Vector3 downFinishLine;
    [SerializeField] private Vector3 leftFinishLine;
    
    public Vector3 UpFinishLine { get => upFinishLine; }
    public Vector3 RightFinishLine { get => rightFinishLine; }
    public Vector3 LeftFinishLine { get => leftFinishLine; }
    public Vector3 DownFinishLine { get => downFinishLine; }
    
    
}
