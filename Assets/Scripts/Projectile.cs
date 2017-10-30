using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    public Color trailColor;
    public float speed = 70f;
    public float lifeTime = 0.1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);


        this.GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColor);
    }



    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


}
