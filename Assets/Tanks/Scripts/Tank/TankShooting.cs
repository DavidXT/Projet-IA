using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int PlayerNumber = 1;       
    public Rigidbody Shell;            
    public Transform FireTransform;    
    public Slider AimSlider;           
    public AudioSource ShootingAudio;  
    public AudioClip ChargingClip;     
    public AudioClip FireClip;         
    public float MinLaunchForce = 15f; 
    public float MaxLaunchForce = 30f; 
    public float MaxChargeTime = 0.75f;

    /*
    private string FireButton;         
    private float CurrentLaunchForce;  
    private float ChargeSpeed;         
    private bool Fired;                


    private void OnEnable()
    {
        CurrentLaunchForce = MinLaunchForce;
        AimSlider.value = MinLaunchForce;
    }


    private void Start()
    {
        FireButton = "Fire" + PlayerNumber;

        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }
    */

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
    }
}