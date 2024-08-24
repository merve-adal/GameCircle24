using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionGizmo : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), new Vector3(1, 1, 1));
    }
}
