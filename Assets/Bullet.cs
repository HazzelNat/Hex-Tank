using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    TankScript tankScript;
    public GameObject selectedTank;
    public Transform firePoint;

    void Start()
    {
        selectedTank = GameObject.FindGameObjectWithTag("Tank");
        firePoint = selectedTank.transform.Find("FirePoint");

        tankScript = selectedTank.GetComponent<TankScript>();
        
        rb.AddForce(firePoint.right * 1250f);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
        tankScript.alreadyHit = true;
    }   
}
