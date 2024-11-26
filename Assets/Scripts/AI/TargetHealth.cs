using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    [SerializeField] int hp;
    private void Start()
    {
        hp = 100;
    }
    public void TakeDamage(int Damage)
    {
        hp-= Damage;
        if(hp<=0)
        {
            Die();
        }
    }
    void Die()
    {
        print("Target Dead");
    }
}
