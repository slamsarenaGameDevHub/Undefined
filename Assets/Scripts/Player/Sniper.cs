using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    //Player Input
    PlayerInput playerInput;
    InputSystem_Actions inputHandler;


    //Components
    Animator animator;
    RecoilSystem1 _recoil;
    CinemachineVirtualCamera _cam;
    CinemachineVirtualCamera shakeCam;
    Scope gunScope;
    GameObject weaponCam;
    Volume gunVolume;

    //Gun Checks
    bool isReload =false;


    [Header("Gun Parameters")]
    float currentBullet=5;
    [SerializeField] int maxBullet=7;
    [SerializeField] float gunDamage;

    private void Start()
    {
        GetCom();
    }
    void GetCom()
    {
        playerInput = GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();

        animator=GetComponentInParent<Animator>();
        _recoil=GetComponentInParent<RecoilSystem1>();
        _cam=GameObject.FindGameObjectWithTag("Scope Cam").GetComponent<CinemachineVirtualCamera>();
        shakeCam=GameObject.FindGameObjectWithTag("Shake Cam").GetComponent<CinemachineVirtualCamera>();
        gunScope=GetComponent<Scope>();
        gunVolume = GameObject.Find("Gun Volume").GetComponent<Volume>();
    }
    private void Update()
    {
        if (weaponCam == null)
        {
            weaponCam = GameObject.FindGameObjectWithTag("Weapon Camera");
        }
        Scope();
        PlayAnimation();
        StopBreathing();
    }
    void StopBreathing()
    {
        if(HoldBreath()==false)
        {
            shakeCam.enabled = true;
        }
        else
        {
            shakeCam.enabled=false;
        }
    }
    void Scope()
    {
        if(isScoped())
        {
            StartCoroutine(ScopeDelay());
        }
        else
        {
            weaponCam.SetActive(true);
            GameObject.FindGameObjectWithTag("Scope Image").GetComponent<Image>().enabled=false;
            gunVolume.weight = 0;
        }

    }
    IEnumerator ScopeDelay()
    {
        yield return new WaitForSeconds(.25f);
        gunScope.ActivateScope();
        weaponCam.SetActive(false);
        shakeCam.m_Lens.FieldOfView = gunScope.scopeZoom;
        gunVolume.weight = 1;

    }
    bool isScoped()
    {
        if(isReload) return false;
        if(inputHandler.Player.Aim.IsPressed())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool HoldBreath()
    {
        if(isReload || currentBullet<=0)return false;
        if(inputHandler.Player.HoldBreath.IsPressed())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void PlayAnimation()
    {
        if(isReload)
        {
            animator.SetFloat("Motion", 2);
            return;
        }
        else if(isScoped())
        {
            animator.SetFloat("Motion", 1);
        }
        else
        {
            animator.SetFloat("Motion", 0);
            shakeCam.m_Lens.FieldOfView = 60;
            _cam.m_Lens.FieldOfView = 60;
        }
    }

}
