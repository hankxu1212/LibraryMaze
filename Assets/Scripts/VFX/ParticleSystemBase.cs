using UnityEngine;

public class ParticleSystemBase : MonoBehaviour
{
    private ParticleSystem particles;
    private bool isPlaying;

    private void Awake()
    {
        isPlaying = false;
        particles = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        if (!isPlaying)
        {
            particles.Play();
            isPlaying = true;
        }
    }

    public void Stop()
    {
        if (isPlaying)
        {
            particles.Stop();
            isPlaying = false;
        }
    }

    public void Restart()
    {
        Stop();
        Play();
    }
}