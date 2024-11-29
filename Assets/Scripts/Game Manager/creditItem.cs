using UnityEngine;
using TMPro;

public class creditItem : MonoBehaviour
{
    [SerializeField] TMP_Text nameText, roleText, contactText;

    public void SetDetails(string name,string role, string contact)
    {
        nameText.text = name;
        roleText.text = role;
        contactText.text = contact;
    }
}
