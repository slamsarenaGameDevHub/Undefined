using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    public float scopeZoom=40;
    [SerializeField] Sprite scopeSprite;
    [HideInInspector]
    public Image scopeHolder;
    CinemachineVirtualCamera scopeCam;
    void Awake()
    {
        scopeHolder=GameObject.FindGameObjectWithTag("Scope Image").GetComponent<Image>();
        scopeHolder.sprite = scopeSprite;
        scopeCam=GameObject.FindGameObjectWithTag("Scope Cam").GetComponent<CinemachineVirtualCamera>();
        scopeHolder.enabled=false;
    }

    public void ActivateScope()
    {
        scopeCam.m_Lens.FieldOfView = scopeZoom;
        scopeHolder.enabled=true;
    }
}
