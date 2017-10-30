﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEjector : MonoBehaviour {

    public Rigidbody rigidBody;
    public float forceMin = 90;
    public float forceMax = 120;

    float lifeTime = 4;
    float fadeTime = 2;

	// Use this for initialization
	void Start ()
    {
        float force = Random.Range(forceMin, forceMax);
        rigidBody.AddForce(transform.right * force);
        rigidBody.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine(Fade());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifeTime);
        float percent = 0;
        float fadeSpeed = 1 / fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;

        while (percent < 1)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(initialColor, Color.clear, percent);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ground")
        {
            this.GetComponent<Rigidbody>().Sleep();
        }
    }
}
