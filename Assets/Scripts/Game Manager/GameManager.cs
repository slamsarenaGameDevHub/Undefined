using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Player Input
    PlayerInput playerInput;
    InputSystem_Actions inputHandler;

    public static event Action Scored;
    public static event Action Lost;

    [Header("Paused State")]
    [HideInInspector]
    public bool isPaused=false;
    [SerializeField] GameObject pauseOverlay;

    [Header("General Necessities")]
    public GameObject BombPrefab;
    public CinemachineImpulseSource explosionImpulse;

    public static void OnCorrectKill()
    {
        Scored?.Invoke();
    }
    public static void OnWrongKill()
    {
        Lost?.Invoke();
    }
    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
        inputHandler.Player.Pause.performed += ctx => Pause();
    }
    private void OnDisable()
    {
        inputHandler.Player.Disable();
    }

   
    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseOverlay.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseOverlay.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
  
}
