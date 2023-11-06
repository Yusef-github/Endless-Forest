using UnityEngine;
public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource[] coinPickupAudioSources;
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource buttonInteractionAudio;

    [SerializeField] private GameObject audioButtonOn;
    [SerializeField] private GameObject audioButtonOff;

    [HideInInspector] public bool isAudioEnabled = true;

    public static AudioHandler audioHandler;

    private void Awake()
    {
        audioHandler = this;
        PlayBackgroundAudio();
    }

    private void PlayBackgroundAudio()
    {
        if (isAudioEnabled)
        {
            backgroundAudio.Play();
            SetAudioButtonState(true);
        }
    }

    public void PlayRandomCoinPickupSound()
    {
        if (isAudioEnabled && coinPickupAudioSources.Length > 0)
        {
            int randomIndex = Random.Range(0, coinPickupAudioSources.Length);
            coinPickupAudioSources[randomIndex].Play();
        }
    }

    public void AudioState()
    {
        isAudioEnabled = !isAudioEnabled;

        if (isAudioEnabled)
            PlayBackgroundAudio();

        else
        {
            backgroundAudio.Stop();
            SetAudioButtonState(false);
        }
    }

    public void ButtonInteraction()
    {
        if (isAudioEnabled)
            buttonInteractionAudio.Play();
    }

    private void SetAudioButtonState(bool enabled)
    {
        audioButtonOn.SetActive(enabled);
        audioButtonOff.SetActive(!enabled);
    }
}