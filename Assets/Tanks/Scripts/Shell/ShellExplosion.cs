using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask TankMask;
    public ParticleSystem ExplosionParticles;       
    public AudioSource ExplosionAudio;              
    public float MaxDamage = 100f;                  
    public float ExplosionForce = 1000f;            
    public float MaxLifeTime = 2f;                  
    public float ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        return 0f;
    }
}