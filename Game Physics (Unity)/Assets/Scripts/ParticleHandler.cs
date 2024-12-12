using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    //public enum ParticleType { Glass, Wood, Stone }
    //public ParticleType particleType;

    //public ParticleSystem[] particles;
    public ParticleSystem woodShards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    /*public void TakeDamage(float collisionForce)
    {
        // Define damage scaling for each material type
        float damageMultiplier = particleType switch
        {
            ParticleType.Glass => 2.0f,
            ParticleType.Wood => 1.0f,
            ParticleType.Stone => 0.5f,
            _ => 1.0f
        };
    }

    private void UpdateMaterialState()
    {
        // Determine the state based on the percentage of remaining HP
        hpPercentage = (float)currentHitPoints / maxHitPoints;

        if (ParticleType == ParticleType.Glass)
        {
            spriteRenderer.sprite = stateSprites[0]; // Perfect state
        }
    }
    */
}
