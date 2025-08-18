using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Efeitos Sonoros")]
    public AudioClip somDeClique;
    public AudioClip somDeAcerto;
    public AudioClip somDeErro;
    public AudioClip somDeVitoria;
    public AudioClip somDeTransicao;

    [Header("Música de Fundo")]
    public AudioClip musicaDoMenu;
    public AudioClip musicaDoQuiz;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Quiz")
        {
            TocarMusica(musicaDoQuiz);
        }
        else
        {
            TocarMusica(musicaDoMenu);
        }
    }

    public void TocarMusica(AudioClip musica)
    {
        if (musicSource.isPlaying && musicSource.clip == musica)
        {
            return;
        }

        if (musica != null)
        {
            musicSource.clip = musica;
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void TocarSomClique()
    {
        if (somDeClique != null) sfxSource.PlayOneShot(somDeClique);
    }

    public void TocarSomAcerto()
    {
        if (somDeAcerto != null) sfxSource.PlayOneShot(somDeAcerto);
    }

    public void TocarSomErro()
    {
        if (somDeErro != null) sfxSource.PlayOneShot(somDeErro);
    }

    public void TocarSomVitoria()
    {
        if (somDeVitoria != null) sfxSource.PlayOneShot(somDeVitoria);
    }

    public void TocarSomTransicao()
    {
        if (somDeTransicao != null) sfxSource.PlayOneShot(somDeTransicao);
    }
}