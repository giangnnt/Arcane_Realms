using UnityEngine;

public class KeepParticleAlive : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void DeactiveParticleSystem()
    {
        ParticleSystem.EmissionModule emission = _particleSystem.emission;
        emission.enabled = false;
    }
}
