using UnityEngine;

public class Credits : MonoBehaviour
{
    public Credit[] credits;
    [SerializeField]creditItem m_creditItem;
    Transform parent;

    private void OnEnable()
    {
        parent = GameObject.Find("Content").transform;
        foreach (Credit c in credits)
        {
            creditItem item=   Instantiate(m_creditItem, parent);
            item.SetDetails(c.Name,c.Role,c.Contact);
        }
    }
}
[System.Serializable]
public class Credit 
{
    public string Name;
    public string Role;
    public string Contact;
}

