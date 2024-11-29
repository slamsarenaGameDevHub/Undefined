using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float smoothTime = 5;
    [SerializeField] float camSensitivity = 5;
    GameManager gameManager;
    private void OnEnable()
    {
        gameManager=FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gameManager.isPaused) return;   
        float mouseX=Input.GetAxisRaw("Mouse X")*camSensitivity;
        float mouseY=Input.GetAxisRaw("Mouse Y")*camSensitivity;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation=Quaternion.Slerp(transform.rotation, targetRotation,smoothTime*Time.deltaTime);
    }
}
