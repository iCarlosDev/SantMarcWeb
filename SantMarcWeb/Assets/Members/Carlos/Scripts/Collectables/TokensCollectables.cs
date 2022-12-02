using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokensCollectables : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float time;
    [SerializeField] private Transform newPoint;
    [SerializeField] private Transform token;
    [SerializeField] private bool isUp;

    private void Awake()
    {
        angle = 0.5f;
        time = 2;
    }

    private void Update()
    {
        TokenMovement();
    }

    private void TokenMovement()
    {
        //POSITION
        var TokenPosition = token.position;
        var newPointPOS = newPoint.position;
        
        if (TokenPosition.Equals(newPointPOS))
        {
            isUp = !isUp;

            if (isUp)
            {
                newPointPOS = new Vector3(newPointPOS.x, newPointPOS.y - 1, newPointPOS.z);
            }
            else
            {
                newPointPOS = new Vector3(newPointPOS.x, newPointPOS.y + 1, newPointPOS.z);
            }
        }
        
        TokenPosition = Vector3.Lerp(TokenPosition, newPointPOS, time * Time.deltaTime);


        //ROTATION
        var TokenVector = transform.up;
        
        gameObject.transform.RotateAround(TokenPosition, TokenVector, angle);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            GetComponentInParent<CollectablesSpawner>().SpawnCollectablesL.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
