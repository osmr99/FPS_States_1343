using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum MovingObjectStates
{
    wander,
    pursue,
    attack,
    recovery
}

public class Enemy : MonoBehaviour
{
    [SerializeField] MovingObjectStates currentState;
    Vector3 startingLocation;
    [SerializeField] float wanderRange;
    [SerializeField] float playerSightRange;
    [SerializeField] float playerAttackRange;
    [SerializeField] float currentStateElapsed;
    [SerializeField] float recoveryTime;


    public Rigidbody Rigidbody { get; private set; }
    Vector3 origin;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        origin = transform.position;
        startingLocation = origin;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case MovingObjectStates.wander:
                break;

            case MovingObjectStates.pursue:
                break;

            case MovingObjectStates.attack:
                break;

            case MovingObjectStates.recovery:
                break;
        }
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse);
    }

    public void Respawn()
    {
        transform.position = origin;
        Rigidbody.AddForce(Vector3.zero, ForceMode.Impulse);
        Rigidbody.velocity = Vector3.zero;
    }
}
