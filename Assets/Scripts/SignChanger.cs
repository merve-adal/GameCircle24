using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignChanger : MonoBehaviour
{
    [SerializeField] private Sign sign;
    public Sign Sign { get => sign; }

    private int signLength = System.Enum.GetValues(typeof(Way)).Length;

    public void ChangeSign(Sign _sign)
    {   
        sign = _sign;

        int signNum = (int)sign;
        for (int i = 0; i < signLength; i++) {
            if (signNum == i)
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
