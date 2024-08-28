using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignChanger : MonoBehaviour
{
    private Sign sign;

    public void ChangeSign(Sign _sign)
    {
        sign = _sign;
        GetComponentInChildren<SignContainer>().ToggleSignObjectByIndex((int)sign);
    }
}
