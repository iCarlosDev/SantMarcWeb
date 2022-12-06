using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Nacho : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _navMeshAgent.SetDestination(GameObject.Find("ExitPoint").transform.position);
        _animator.SetBool(IsWalking, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var exitPoint2 = GameObject.FindWithTag("ExitPoint2");
        
        if (other.CompareTag("ExitPoint"))
        {
            Debug.Log("BIEEN");
            exitPoint2.GetComponent<SphereCollider>().enabled = true;
           _navMeshAgent.SetDestination(exitPoint2.transform.position);
        }

        if (other.CompareTag("ExitPoint2"))
        {
            _animator.SetBool(IsWalking, false);
            _navMeshAgent.speed = 0;
        }
    }
}
