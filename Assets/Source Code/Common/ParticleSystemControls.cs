using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic playback controls to a chosen particle system.
/// Attach script to GameObject with particle system component.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemControls : MonoBehaviour
{
    private ParticleSystem mainParticleSystem;
    public ParticleSystem MainParticleSystem { get { return mainParticleSystem; } }

    [SerializeField]
    [Tooltip("Check if child particle systems in the main one should play.")]
    private bool includeChildParticleSystems = false;
    public bool IncludeChildParticleSystems { get { return includeChildParticleSystems; } set { includeChildParticleSystems = value; } }

    private void Awake()
    {
        mainParticleSystem = this.GetComponent<ParticleSystem>();
    }

    #region Basic Particle System Controls

    public void StartParticleSystem()
    {
        mainParticleSystem.Play(includeChildParticleSystems);
    }
    public void PauseParticleSystem()
    {
        mainParticleSystem.Pause(includeChildParticleSystems);
    }
    public void StopParticleSystem()
    {
        mainParticleSystem.Stop(includeChildParticleSystems);
    }
    public void RestartParticleSystem()
    {
        mainParticleSystem.Simulate(0f, includeChildParticleSystems, true);
    }

    #endregion
    #region Interactive Particle System Controls

    public void PlayOnTrigger()
    {
        bool currentlyPlaying = mainParticleSystem.isPlaying;

        if (currentlyPlaying)
        {
            RestartParticleSystem();
            StartParticleSystem();
        }
        else
        {
            StartParticleSystem();
        }
    }

    public void PlayOnInput(string inputName)
    {
        bool currentlyPlaying = mainParticleSystem.isPlaying;

        if (Input.GetButton(inputName))
        {
            if (!currentlyPlaying)
            {
                StartParticleSystem();
            }
        }
        else if (Input.GetButtonUp(inputName))
        {
            if (currentlyPlaying)
            {
                StopParticleSystem();
            }
        }
    }

    public void PlayOnInputDown(string inputName)
    {
        if (Input.GetButtonDown(inputName))
        {
            PlayOnTrigger();
        }
    }

    #endregion
}
