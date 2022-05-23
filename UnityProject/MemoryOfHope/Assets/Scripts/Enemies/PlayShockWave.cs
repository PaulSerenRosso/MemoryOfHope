using UnityEngine;

public class PlayShockWave : MonoBehaviour
{
    [SerializeField] private ParticleSystem shockwave;
    
    public void ShockwavePlay()
    {
        shockwave.Play();
    }
}
