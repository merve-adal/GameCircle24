using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignContainer : MonoBehaviour
{
    private int signLength = System.Enum.GetValues(typeof(Sign)).Length;

    public void ToggleSignObjectByIndex(int index)
    {
        for (int i = 0; i < signLength; i++)
        {
            if (index == i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
