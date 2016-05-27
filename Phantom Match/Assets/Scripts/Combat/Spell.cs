using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public float lifeTime;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > lifeTime)
        {
            Explode();
        }
    }

    private void Explode() //Do some fancy animation/particle effect
    {
        Destroy(gameObject);
    }
}
