using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum MovingObjectStates
{
    wander,
    pursue,
    attack,
    recovery
}

public class Enemy : MonoBehaviour
{
    [SerializeField] public MovingObjectStates currentState;
    Vector3 startingLocation;
    [SerializeField] float wanderRange;
    float wanderInterval;
    [SerializeField] float playerSightRange;
    [SerializeField] float playerAttackRange;
    [SerializeField] float currentStateElapsed;
    [SerializeField] float recoveryTime;
    [SerializeField] float hurtTime = 0.75f; // Whenever the enemy gets damaged, they enter a stunt state for 
    //                                          this amount of seconds.
    NavMeshAgent agent;

    public Rigidbody Rigidbody { get; private set; }
    Vector3 origin;
    float elapsed_one = 0;
    float elapsed_two = 0;

    public bool wanderIndex = false;
    public bool pursueIndex = false;

    public bool isPursuing = false;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        startingLocation = origin;
        agent.SetDestination(startingLocation);
        //agent.SetDestination(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        elapsed_one += Time.deltaTime;
        elapsed_two += Time.deltaTime;
        if (elapsed_one > hurtTime && Rigidbody.isKinematic == false)
        {
            Rigidbody.isKinematic = true;
            agent.enabled = true;
        }


        switch (currentState)
        {
            case MovingObjectStates.wander:
                Debug.Log("state 0");
                startWander();
                break;

            case MovingObjectStates.pursue:
                Debug.Log("state 1");
                isPursuing = true;
                startPursue();
                break;

            case MovingObjectStates.attack:
                break;

            case MovingObjectStates.recovery:
                break;

        }

        Debug.Log("is pursuing = " + isPursuing);
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        if(Rigidbody.isKinematic)
        {
            agent.enabled = false;
            Rigidbody.isKinematic = false;
            elapsed_one = 0;
            GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse);
        }
        else
        {
            agent.enabled = false;
            elapsed_one = 0;
            GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse);
        }

    }

    public void Respawn()
    {
        transform.position = origin;
        Rigidbody.AddForce(Vector3.zero, ForceMode.Impulse);
        Rigidbody.velocity = Vector3.zero;
    }

    Vector3 Wander()
    {
        Vector3 offset = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));

        NavMeshHit hit;

        bool gotPoint = NavMesh.SamplePosition(offset, out hit, 1, NavMesh.AllAreas);

        if (gotPoint)
        {
            Debug.Log("Success!");
            return hit.position;
        }
        else
        {
            Debug.Log("Fail");
            return startingLocation;
        }
    }

    void randomWanderRate()
    {
        wanderInterval = Random.Range(2, 4);
    }

    public void startWanderIndex()
    {
        currentState = MovingObjectStates.wander;
    }

    public void startPursueIndex()
    {
        currentState = MovingObjectStates.pursue;
    }

    public void startWander()
    {
        if (elapsed_two > wanderInterval && isPursuing == false)
        {
            agent.SetDestination(Wander());
            randomWanderRate();
            elapsed_two = 0;
        }
    }

    public void startPursue()
    {

    }

 
}
