using UnityEngine;

public class Terrorist : MonoBehaviour,ITakeDamage
{
    [Header("Movement")]
    public Transform path;
    [SerializeField] GameObject ragdoll;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void DealDamage()
    {
        print("Enemy Killed");
    }
}
