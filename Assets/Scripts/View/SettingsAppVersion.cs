using TMPro;
using UnityEngine;
using System;


public class SettingsAppVersion : MonoBehaviour
{

    private void Start()
    {
        var currentYear = DateTime.Now.Year;
        GetComponent<TextMeshProUGUI>().text = $"(c) UnitedITForce, {currentYear}\nv " + Application.version;
    }
}

