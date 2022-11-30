using System;
using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts;
using Unity.Mathematics;
using UnityEngine;

public class NachoChairRotation : MonoBehaviour
{
    //Variables
    [SerializeField] private bool canRotate;
    [SerializeField] private float damping;
    [SerializeField] private Animator nachoAnimator;
    [SerializeField] private Transform player;
    [SerializeField] private Transform monitor;
    private static readonly int IsOnRange = Animator.StringToHash("IsOnRange");


    private void Awake()
    {
        nachoAnimator = GameObject.Find("NPC_NACHO").GetComponent<Animator>();
        monitor = GameObject.Find("Monitor").transform;
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        if (canRotate && nachoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sitting Idle"))
        {
            var lookPos = player.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
        else
        {
            var lookPos = monitor.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = true;
            nachoAnimator.SetBool(IsOnRange, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = false;
            nachoAnimator.SetBool(IsOnRange, false);
        }
    }
}
