using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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

    [SerializeField] Vector3 boxSize=new Vector3(3,3,3);

    BoxCollider _collider;
    private void Start()
    {
        _collider=GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _collider.size = boxSize;
    }
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
