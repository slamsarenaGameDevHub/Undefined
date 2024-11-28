using Cinemachine;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action Scored;
    public static event Action Lost;

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
