using UnityEngine;

public class OpenPrivacyPolicy : MonoBehaviour
{
    private string _privacyURL = "https://docs.google.com/document/d/1zeeJ2kKRbaKkHCSyXlcAdQ6rp8651tCChpjEkYAQgHU/edit?usp=sharing";

    public void OpenPrivacyPolicyURL()
    {
        Application.OpenURL(_privacyURL);
    }
}
