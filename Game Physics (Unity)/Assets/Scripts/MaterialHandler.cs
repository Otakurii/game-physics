using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler : MonoBehaviour
{
    public enum MaterialType { Glass, Wood, Stone }
    public MaterialType materialType;

    public int maxHitPoints = 20; // Maximum HP of the material
    private int currentHitPoints;
    float hpPercentage;

    public Sprite[] stateSprites; // Array of sprites for different states
    private SpriteRenderer spriteRenderer;

    public GameObject explosionEffect;  //explosion gas effect from animator
    public ParticleSystem particleEffect;   //particle system

    Animator anim;  //for explosion effect

    void Start()
    {
        currentHitPoints = maxHitPoints; // Initialize HP
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleEffect = GetComponentInChildren<ParticleSystem>();
        UpdateMaterialState(); // Set initial sprite
    }


    public void TakeDamage(float collisionForce)
    {
        // Define damage scaling for each material type
        float damageMultiplier = materialType switch
        {
            MaterialType.Glass => 2.0f,
            MaterialType.Wood => 1.0f,
            MaterialType.Stone => 0.5f,
            _ => 1.0f
        };

        // Calculate damage
        int damage = Mathf.RoundToInt(collisionForce * damageMultiplier);

        // Reduce HP
        currentHitPoints -= damage;

        // Clamp HP to prevent negative values
        currentHitPoints = Mathf.Max(currentHitPoints, 0);

        // Update material state
        UpdateMaterialState();

        // Destroy the material if HP reaches zero
        if (currentHitPoints <= 0)
        {
            ExplosionEffect();
            //DestroyMaterial();
            StartCoroutine(ParticleEffect());
        }

        //Debug.Log($"Collision Force: {collisionForce}, Damage: {damage}, Remaining HP: {currentHitPoints}");
    }

    private void UpdateMaterialState()
    {
        // Determine the state based on the percentage of remaining HP
        hpPercentage = (float)currentHitPoints / maxHitPoints;

        if (hpPercentage > 0.90f)
        {
            spriteRenderer.sprite = stateSprites[0]; // Perfect state
        }
        else if (hpPercentage > 0.75f)
        {
            spriteRenderer.sprite = stateSprites[1]; // Slightly damaged
        }
        else if (hpPercentage > 0.40f)
        {
            spriteRenderer.sprite = stateSprites[2]; // Moderately damaged
        }
        else
        {
            spriteRenderer.sprite = stateSprites[3]; // Almost broken
        }
        
    }

    private void ExplosionEffect()
    {
        //explosion effect
        //if rock collide w enemy or even ground, explode
        if(hpPercentage <= 0.00f)
        {
            GameObject newEffect;
            newEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            //after some distance, destroy the object
            Destroy(newEffect.gameObject, 0.5f);
        }

        //particle effect for diff material
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the object colliding has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the box!");

            // Get the collision normal (opposite direction of impact)
            Vector2 collisionNormal = other.contacts[0].normal;

            // Adjust the rotation of the particle system to emit particles in the opposite direction
            AlignParticleSystemToCollision(collisionNormal);

            StartCoroutine(ParticleEffect());
        }
    }

    private void AlignParticleSystemToCollision(Vector2 normal)
    {
        // Convert collision normal into an angle in degrees
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

        // Rotate the particle system
        particleEffect.transform.rotation = Quaternion.Euler(angle + 90, 0, 0); // Add 180 to invert the direction
    }

    IEnumerator ParticleEffect()
    {
        // Play the particle system
        if (particleEffect != null)
        {
            particleEffect.transform.position = transform.position; 
            particleEffect.Play();
            Debug.Log("Particle system should now be playing!");


            if (currentHitPoints <= 0)
            {
                Debug.Log("destroying game object");
                yield return new WaitForSeconds(particleEffect.main.startLifetime.constantMax - 1.2f);
                Destroy(gameObject);
            }
            
        }
        else
        {
            Debug.LogWarning("Particle system is not assigned!");
        }
    }
}
