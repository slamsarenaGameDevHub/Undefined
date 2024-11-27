using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),(typeof(DestroyGameObject)))]
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

    [Header("Waypoint Tracker")]
    int currentWaypoint = 0;
    [SerializeField] GameObject ragdoll;

    [Header("Movement")]
    public Transform Path;
    List<Transform> nodes;

    [SerializeField] float changeDistance = 8f;
    [SerializeField] float minSpeed = 1, maxSpeed = 2.5f, walkThreshold = 2.4f;
    float agentSpeed;
    float speed;

    Vector3 lastPos;
    void OnEnable()
    {
        GetCom();
        GetWaypoint();
        GameManager.DamageChanged += Affected;
    }
    private void OnDisable()
    {
        GameManager.DamageChanged -= Affected;
    }
    void GetCom()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Agent.speed = 1;
        lastPos = transform.position;
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
        UpdateNode();
        Move();
        GetSpeed();
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
    public void DealDamage(int damage)
    {
        print(damage);
        Instantiate(ragdoll, transform.position, transform.rotation);
        GameManager.OnCollect();
        Destroy(gameObject);
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
    void Affected()
    {
        print("Npc Shot");
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
