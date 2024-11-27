using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    internal enum DestroyType
    {
        Timed,
        Entry
    }
    [SerializeField] DestroyType _destroyType = DestroyType.Entry;

    float countDown;
    [SerializeField] float _Delay=2;
    private void Update()
    {
        switch (_destroyType)
        {
            case DestroyType.Timed:
                countDown-=Time.deltaTime;
                if(countDown<=0)
                {
                    Destroy(gameObject);
                }
                break;
            case DestroyType.Entry:

                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_destroyType != DestroyType.Entry) return;
        if(other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
