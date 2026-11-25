using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Efeitos")]
    [SerializeField] private int sfxSourcesCount = 16;  

    [Header("Sons 3D")]
    [SerializeField] private float minDistance3D = 1f;  
    [SerializeField] private float maxDistance3D = 30f; 

    private AudioSource musicSource;
    private AudioSource[] sfxSources;
    private int nextSfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        
        sfxSources = new AudioSource[sfxSourcesCount];
        
        for (int i = 0; i < sfxSourcesCount; i++)
        {
            GameObject child = new GameObject("SFX " + i);
            child.transform.SetParent(transform);

            AudioSource source = child.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.minDistance = minDistance3D;
            source.maxDistance = maxDistance3D;
            source.rolloffMode = AudioRolloffMode.Linear;

            sfxSources[i] = source;
        }
    }
    
    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        AudioSource source = GetNextSource();
        source.spatialBlend = 0f; // 0 = 2D
        Play(source, clip, volume, pitch);
    }
    
    public void PlaySFXAt(AudioClip clip, Vector3 position, float volume = 1f, float pitch = 1f)
    {
        AudioSource source = GetNextSource();
        source.spatialBlend = 1f; // 1 = 3D
        source.transform.position = position;
        Play(source, clip, volume, pitch);
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    private AudioSource GetNextSource()
    {
        AudioSource source = sfxSources[nextSfxSource];
        nextSfxSource = (nextSfxSource + 1) % sfxSources.Length;
        return source;
    }

    private void Play(AudioSource source, AudioClip clip, float volume, float pitch)
    {
        if (clip == null) return;

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}
