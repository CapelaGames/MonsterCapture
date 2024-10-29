using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Investigating,
        Chasing,
        Attack,
        Captured
    }
    public State state;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        NextState();
    }
    void NextState()
    {
        switch (state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Investigating:
                StartCoroutine(InvestigatingState());
                break;
            case State.Chasing:
                StartCoroutine(ChasingState());
                break;
            case State.Attack:
                break;
            case State.Captured:
                break;
            default:
                Debug.LogError("State doesn't exist");
                break;
        }
    }

    IEnumerator PatrolState()
    {
        //Setup /entry point / Start()/Awake()
        Debug.Log("Entering Patrol State");


        while(state == State.Patrol) // "Update() loop"
        {
 
            yield return null; // Wait for a frame
        }


        //tear down/ exit  point  / OnDestory()
        Debug.Log("Exiting Patrol State");
        NextState();
    }


    IEnumerator InvestigatingState()
    {
        //Setup /entry point / Start()/Awake()
        Debug.Log("Entering Investigating State");
        float startTime = Time.time;
        float deltaSum = 0;

        while (state == State.Investigating) // "Update() loop"
        {
            deltaSum += Time.deltaTime;
            yield return null; // Wait for a frame
        }

        float endTime = Time.time - startTime;
        Debug.Log("DeltaSum = " + deltaSum + " | End Time = " + endTime);

        //tear down/ exit  point  / OnDestory()
        Debug.Log("Exiting Investigating State");
        NextState();
    }


    IEnumerator ChasingState()
    {
        //Setup /entry point / Start()/Awake()
        Debug.Log("Entering Chasing State");


        while (state == State.Chasing) // "Update() loop"
        {
            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 20f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2, wave);


            float shimmy = Mathf.Cos(Time.time * 30f) * 10f + 10f;
            //choose transform movement or rigidbody movement
            //transform.position += transform.right * shimmy * Time.deltaTime;
            rb.AddForce(Vector3.right * shimmy);

            yield return null; // Wait for a frame
        }


        //tear down/ exit  point  / OnDestory()
        Debug.Log("Exiting Chasing State");
        NextState();
    }
}
