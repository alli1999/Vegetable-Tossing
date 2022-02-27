using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toss : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = true;
    public GameObject pan;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(isGrounded);
        if (pan.transform.rotation.y >= 0.75f && isGrounded)
        {
            rb.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if(collision.gameObject.name == "Frying_Pan_2")
        {
            Debug.Log("Pan Enyeres");
            isGrounded = true;
            Debug.Log(isGrounded);
        }
    }
}
