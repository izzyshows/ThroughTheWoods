using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int bombState = 0; //0=idle, 1=fuse
    private float fuseTimer;
    public float fuseLength = 2.0f;
    private ParticleSystem sparks;
    private ParticleSystem explosion;
    private Component[] comparray;

    void Start() {
        comparray = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in comparray) {
            if (p.gameObject.name == "Explosion") explosion = p;
            if (p.gameObject.name == "Sparks") sparks = p;
        }
        bombState = 0;
        fuseTimer = fuseLength;
        // sparks = GetComponentInChildren<ParticleSystem>();
        sparks.Stop();
    }

    void Update() {
        if (bombState == 1) {
            fuseTimer -= Time.deltaTime;
            if (fuseTimer <= 0.0f) {
                explosion.transform.SetParent(null);
                explosion.Play();
                DamageNearbyObjects(gameObject.transform);
                Destroy(gameObject);
            }
            if (sparks.isStopped) {
                sparks.Play();
            }
        }
    }

    void DamageNearbyObjects(Transform tr) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tr.position, 1.5f);
        for(int i=0; i<colliders.Length; i++) {
            if(colliders[i].gameObject.tag == "Spiker") {
                Destroy(colliders[i].gameObject);
            }
            if (colliders[i].gameObject.tag == "Robot") {
                Destroy(colliders[i].gameObject);
            }
        }
    }
}
