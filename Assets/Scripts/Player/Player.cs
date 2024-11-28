using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
    [SerializeField] float camSensitivity = 30;

    [Header("Voice")]
    int clipTracker;
        
    AudioSource playerAudioSource;
    AudioClip activeClip;
    [SerializeField] List<AudioClip> voiceClips = new List<AudioClip>();
    private void OnEnable()
    {
        playerInput=GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
        playerAudioSource=GetComponent<AudioSource>();
        playerAudioSource.loop=false;
        playerAudioSource.clip=activeClip;
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
        activeClip = voiceClips[clipTracker];
        playerAudioSource.Play();
    }
}
