using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelInfo : MonoBehaviour
{

    [SerializeField] TMP_Text levelNameText;
    [SerializeField] TMP_Text levelLivesText;

    public string LevelName { set => levelNameText.text = value; }
    public string LevelLives { set => levelLivesText.text = value; }

}
