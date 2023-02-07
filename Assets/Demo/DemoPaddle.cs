using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoPaddle : MonoBehaviour
{
    public float unitsPerSecond = 3f;

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        Vector3 force = Vector3.right * horizontalValue; // * unitsPerSecond * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"we hit {collision.gameObject.name}");
        
        // get reference to paddle collider
        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxX = bounds.max.x;
        float minX = bounds.min.x;

        Debug.Log($"maxX = {maxX}, minY = {minX}");
        Debug.Log($"x pos of ball is {collision.transform.position.x}");

        Quaternion rotation = Quaternion.Euler(0f, 0f, -60f);
        Vector3 bounceDirection = rotation * Vector3.up;
        
        Rigidbody rb = collision.rigidbody;
        rb.AddForce(bounceDirection * 1000f, ForceMode.Force);
    }
}
