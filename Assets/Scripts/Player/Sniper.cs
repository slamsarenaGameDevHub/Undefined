using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class Sniper : MonoBehaviour
{
    //Player Input
    PlayerInput playerInput;
    InputSystem_Actions inputHandler;


    //Components
    Animator animator;
    Volume gunVolume;
    CinemachineVirtualCamera _cam;
    CinemachineVirtualCamera shakeCam;
    CinemachineImpulseSource impulseCam;

    Scope gunScope;
    ScoreDisplay scoreDisplay;
    GameObject weaponCam;
    //Gun Checks
    bool isReload =false,canShoot=true;
    [SerializeField] Image dCrossHair;
    AudioSource gunSource;


    [Header("Gun Parameters")]
    float currentBullet=5;
    [SerializeField] int maxBullet=7;
    [SerializeField] int damage = 150;
    [SerializeField] float _gunImpact=3;
    [SerializeField] float _gunImpulse=1;
    [SerializeField] float _maxRange=1000;
    [SerializeField] float scareRange=30;
    private void Start()
    {
        GetCom();
    }
    void GetCom()
    {
        playerInput = GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        animator=GetComponentInParent<Animator>();
        gunSource = GetComponent<AudioSource>();
        _cam=GameObject.FindGameObjectWithTag("Scope Cam").GetComponent<CinemachineVirtualCamera>();
        shakeCam=GameObject.FindGameObjectWithTag("Shake Cam").GetComponent<CinemachineVirtualCamera>();
        gunScope=GetComponent<Scope>();
        gunVolume = GameObject.Find("Gun Volume").GetComponent<Volume>();
        impulseCam=GetComponent<CinemachineImpulseSource>();
        inputHandler.Player.Attack.performed += ctx => Fire();
        scoreDisplay = FindFirstObjectByType<ScoreManager>().scoreHolder;
    }
    private void Update()
    {
        if(currentBullet<=0)
        {
            weaponCam.SetActive(true);
            GameObject.FindGameObjectWithTag("Scope Image").GetComponent<Image>().enabled = false;
            gunVolume.weight = 0;
            dCrossHair.enabled = true;
            isReload = true;
        }
        else
        {
            isReload = false;
        }
        if (weaponCam == null)
        {
            weaponCam = GameObject.FindGameObjectWithTag("Weapon Camera");
        }
        Scope();
        PlayAnimation();
        StopBreathing();
    }
    void Fire()
    {
        if (currentBullet <= 0 || !canShoot) return;
        shakeCam.Priority = 0;
        gunSource.Play();
        currentBullet--;
        animator.SetTrigger("shoot");
       
        impulseCam.GenerateImpulse(_gunImpulse);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _maxRange))
        {
            //Get Collider Info
            ITakeDamage target=hit.transform.GetComponent<ITakeDamage>();
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            
            if (target != null)
            {
                target.DealDamage(damage);
                ScoreDisplay score=Instantiate(scoreDisplay,transform.position,transform.rotation);
                score.SetScore(damage);
            }
            else
            {
                Collider[] col = Physics.OverlapSphere(hit.point, scareRange);
                foreach (Collider carrier in col)
                {
                    IScare scaredPerson = carrier.GetComponent<IScare>();
                    if(scaredPerson!= null)
                    {
                        scaredPerson.Scare();
                    }
                }
            }
            if (rb != null)
            {
                rb.AddForce(hit.normal * _gunImpact * Time.deltaTime);
            }
         
        }
        canShoot = false;
    }
    
    public void CanShoot()
    {
        if (currentBullet <= 0) return;
        canShoot=true;
        shakeCam.Priority = 2;
    }
    public void Reload()
    {
        currentBullet = maxBullet;
        canShoot = true;
        animator.ResetTrigger("isReload");
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
        if (isReload) return;
        if(isScoped())
        {
            StartCoroutine(ScopeDelay());
        }
        else
        {
            weaponCam.SetActive(true);
            GameObject.FindGameObjectWithTag("Scope Image").GetComponent<Image>().enabled=false;
            gunVolume.weight = 0;
            dCrossHair.enabled = true;

        }

    }
    IEnumerator ScopeDelay()
    {
        yield return new WaitForSeconds(.25f);
        gunScope.ActivateScope();
        weaponCam.SetActive(false);
        shakeCam.m_Lens.FieldOfView = gunScope.scopeZoom;
        gunVolume.weight = 1;
        dCrossHair.enabled = false;

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
            animator.SetTrigger("isReload");
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
public interface ITakeDamage 
{
    public void DealDamage(int damage);
}
public interface IScare
{
    public void Scare();
}


