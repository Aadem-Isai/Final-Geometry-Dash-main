using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            Debug.Log("MusicManager created!");
            
            // Play music for current scene immediately
            PlayMusicForScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }
    
    void PlayMusicForScene(string sceneName)
    {
        Debug.Log("PlayMusicForScene called for: " + sceneName);
        
        AudioClip clipToPlay = null;
        
        switch(sceneName)
        {
            case "MainMenu":
                clipToPlay = menuMusic;
                Debug.Log("Menu music assigned");
                break;
            case "Level1":
                clipToPlay = level1Music;
                Debug.Log("Level1 music assigned");
                break;
            case "Level2":
                clipToPlay = level2Music;
                Debug.Log("Level2 music assigned");
                break;
            case "Level3":
                clipToPlay = level3Music;
                Debug.Log("Level3 music assigned");
                break;
            default:
                Debug.Log("No music found for scene: " + sceneName);
                break;
        }
        
        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.Stop();
            audioSource.clip = clipToPlay;
            audioSource.Play();
            Debug.Log("Playing: " + clipToPlay.name);
        }
        else if (clipToPlay == null)
        {
            Debug.Log("ERROR: clipToPlay is NULL! Did you assign the music in Inspector?");
        }
    }
    
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}