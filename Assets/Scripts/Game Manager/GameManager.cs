using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action DamageChanged;

    public static void OnCollect()
    {
        DamageChanged?.Invoke();
    }



    [Header("General Necessities")]
    public GameObject BombPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
