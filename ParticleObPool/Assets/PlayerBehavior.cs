using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody PlayerBody;
    public ParticleObjPool ParticleObjPool = ParticleObjPool.Instance;

    private bool _isGrounded = false;
    
    void Update()
    {
        float acceleration = 150f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                PlayerBody.AddForce(Vector3.up * 300);
                
                Debug.Log("Jump");
            }
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerBody.velocity += Vector3.forward * acceleration * Time.fixedDeltaTime;
        }
       


        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerBody.velocity += Vector3.back * acceleration * Time.fixedDeltaTime;
        }
       


        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerBody.velocity += Vector3.right * acceleration * Time.fixedDeltaTime;
        }
       

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerBody.velocity += Vector3.left * acceleration * Time.fixedDeltaTime;
        }
       


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Plane"))
        {
            GameObject obj = ParticleObjPool.SpawnFromPool("Spark", transform.position, transform.rotation);
            StartCoroutine(ParticleObjPool.RecycleObject("Spark", obj));
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Plane"))
        {
            GameObject obj = ParticleObjPool.SpawnFromPool("Explosion", transform.position, transform.rotation);
            StartCoroutine(ParticleObjPool.RecycleObject("Explosion", obj));
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Plane"))
        {
            _isGrounded = true;
        }
    }
}
