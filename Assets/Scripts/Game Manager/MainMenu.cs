using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Gun Rotation")]
    [SerializeField] Transform GunBag;
    [SerializeField] float rotateSpeed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        ChooseSpawnPos();
    }
    void ChooseSpawnPos()
    {
        PlayerPrefs.SetInt("Spawn Position", Random.Range(0, 11)); 
    }
    void RotateGun()
    {
        GunBag.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
