using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour, ITakeDamage,IScare
{
    internal enum DirectionOfMovement
    {
        Forward,
        Backward
    }
    [SerializeField] DirectionOfMovement _directionOfMovement;

    [Header("Components")]
    NavMeshAgent Agent;
    Animator animator;
    [SerializeField]AnimatorOverrideController npcController;

    [Header("Waypoint Tracker")]
    int currentWaypoint = 0;
    [SerializeField] GameObject ragdoll;

    [Header("Movement")]
    public Transform Path;
    List<Transform> nodes;

    [Tooltip("Change distance is the minimum distance an npc must reach before changing waypoint, stop duration is the delay before the npc stops moving,and the stopped delay is how long the npc is idle")]
    [SerializeField] float changeDistance = 8f,stopDuration=37,stoppedDelay=7;
    [SerializeField] float minSpeed = 1, maxSpeed = 2.5f, walkThreshold = 2.4f;
    float agentSpeed, stopCountDown,speed;

    Vector3 lastPos;


    bool hasDied=false;
    void OnEnable()
    {
        GetCom();
        GetWaypoint();
    }
    private void OnDisable()
    {
    }
    void GetCom()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = npcController;
        Agent.speed = 1;
        lastPos = transform.position;
        stopCountDown = stopDuration;
    }

    void GetWaypoint()
    {
        Transform[] waypoint = Path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < waypoint.Length; i++)
        {
            if (waypoint[i] != Path)
            {
                nodes.Add(waypoint[i]);
            }
        }
    }

    void Update()
    {
        GetWaypoint();
        UpdateNode();
        Move();
        GetSpeed();
        Action();
    }
    void UpdateNode()
    {
        agentSpeed = Random.Range(minSpeed, maxSpeed);
        switch (_directionOfMovement)
        {
            case DirectionOfMovement.Forward:
                if (Vector3.Distance(transform.position, nodes[currentWaypoint].position) <= changeDistance)
                {
                    Agent.speed = agentSpeed;
                    if (currentWaypoint >= nodes.Count - 1)
                    {
                        currentWaypoint = 0;
                    }
                    else
                    {
                        currentWaypoint++;
                    }
                }
                break;
            case DirectionOfMovement.Backward:
                if (Vector3.Distance(transform.position, nodes[currentWaypoint].position) <= changeDistance)
                {
                    Agent.speed = agentSpeed;
                    if (currentWaypoint <= 0)
                    {
                        currentWaypoint = nodes.Count - 1;
                    }
                    else
                    {
                        currentWaypoint--;
                    }
                }
                break;
        }
    }
    void Move()
    {
        Agent.SetDestination(nodes[currentWaypoint].position);
        PlayAnimation();
    }
    void Action()
    {
        stopCountDown -= Time.deltaTime;
        if(stopCountDown<=0)
        {
            Agent.isStopped= true;
            Invoke("ResetState", stoppedDelay);
        }

    }
    void ResetState()
    {
        Agent.isStopped = false;
        stopCountDown = stopDuration;
    }
    public void DealDamage(int damage)
    {
        if (hasDied) return;
        Instantiate(ragdoll, transform.position, transform.rotation);
        GameManager.OnWrongKill();
        Destroy(gameObject);
        hasDied = true;
    }
    void PlayAnimation()
    {
        if (speed >= walkThreshold)
        {
            animator.SetFloat("Motion", 2);
        }
        else if (speed > 0.1f && speed < walkThreshold)
        {
            animator.SetFloat("Motion", 1);
        }
        else
        {
            animator.SetFloat("Motion", 0);
        }
    }
    void GetSpeed()
    {
        speed = Vector3.Distance(transform.position, lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }
    public void Scare()
    {
        minSpeed = maxSpeed;
        Agent.speed = maxSpeed;
    }
    
}
