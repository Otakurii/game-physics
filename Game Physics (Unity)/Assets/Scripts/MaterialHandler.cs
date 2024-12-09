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

    public GameObject explosionEffect; //explosion gas effect 

    Animator anim;  //for explosion effect

    void Start()
    {
        currentHitPoints = maxHitPoints; // Initialize HP
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            DestroyMaterial();
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

    private void DestroyMaterial()
    {
        Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.contacts[0].normal.x > 0.5f){
            Debug.Log("collision!");
        }
    }
}
