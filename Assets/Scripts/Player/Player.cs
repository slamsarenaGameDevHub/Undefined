using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    //Input 
    PlayerInput playerInput;
    InputSystem_Actions inputHandler;

    //Clamp Values
    float xLook,yLook;
    [SerializeField] float yMinLookLimit = -250,yMaxLookLimit=0,xLookLimit=60;

    //Camera Control
    [SerializeField] Slider mouseSlider;
    float camSensitivity
    {
        get
        {
            return mouseSlider.value;
        }
    }

    [Header("Voice")]
    int clipTracker;
        
    AudioSource playerAudioSource;
    [SerializeField] List<AudioClip> voiceClips = new List<AudioClip>();
    private void OnEnable()
    {
        playerInput=GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
        playerAudioSource=GetComponent<AudioSource>();
        playerAudioSource.loop=false;
    }
    void OnDisable()
    {
        inputHandler.Player.Disable();
    }
    private void Update()
    {
        Movement();
        ChangeSoundClip();
    }
    void ChangeSoundClip()
    {
        clipTracker=Random.Range(0,voiceClips.Count);
    }
    void Movement()
    {
        Vector2 LookInput=inputHandler.Player.Look.ReadValue<Vector2>();

        yLook+=LookInput.x*camSensitivity*Time.deltaTime;  // Left To Right Rotation
        yLook = Mathf.Clamp(yLook, -yMinLookLimit, yMaxLookLimit);

        xLook-=LookInput.y*camSensitivity*Time.deltaTime;  //Up down Rotation
        xLook=Mathf.Clamp(xLook, -xLookLimit, xLookLimit);

        transform.localRotation = Quaternion.Euler(xLook, yLook, 0);

    }
    public void PlayVoice()
    {
        playerAudioSource.clip = voiceClips[clipTracker];
        playerAudioSource.Play();
    }
}
