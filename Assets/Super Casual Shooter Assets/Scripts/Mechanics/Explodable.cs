using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damagable))]
public class Explodable : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosion_particle; // particle for explosion
    public AudioClip Explode;
    public float volume = 0.2f;

    [SerializeField]
    private int explosion_range_radius = 10; // at this range all rigidbodies will be affected and all damagables will be damaged

    [SerializeField]
    private int explosion_force = 20; // explosion force on rigid bodies

    [SerializeField]
    private int attack_value = 1; // damage on damagables


    public void nowExplode()
    {
       

        Instantiate(explosion_particle, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(Explode, transform.position, volume);
        foreach (Collider collider in Physics.OverlapSphere(transform.position, explosion_range_radius))
        {
            Rigidbody rigidbody_ = collider.GetComponent<Rigidbody>();
            if (rigidbody_)
            {
                rigidbody_.AddExplosionForce(explosion_force, transform.position, explosion_range_radius);
            }


            Damagable damagable = collider.gameObject.GetComponent<Damagable>();
            if (damagable)
            {
                damagable.takeDamage(attack_value);
            }

        }
        Destroy(gameObject, 0.5f); // destroy this object
        
    }
}

