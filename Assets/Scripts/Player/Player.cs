using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    //Input 
    PlayerInput playerInput;
    InputSystem_Actions inputHandler;

    //Clamp Values
    float xLook,yGain;
    [SerializeField] float yLookLimit = 60,xLookLimit=60;

    //Camera Control
    [SerializeField] float camSensitivity = 30;
    private void OnEnable()
    {
        playerInput=GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
    }
    private void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector2 LookInput=inputHandler.Player.Look.ReadValue<Vector2>();

        float yLook=LookInput.y*camSensitivity*Time.deltaTime;  // Left To Right Rotation
        yGain += yLook;
        yGain = Mathf.Clamp(yGain, -yLookLimit, yLookLimit);
        xLook-=LookInput.x*camSensitivity*Time.deltaTime;  //Up down Rotation
        xLook=Mathf.Clamp(xLook, -xLookLimit, xLookLimit);

        transform.localRotation = Quaternion.Euler(xLook, yGain, 0);

    }
}
