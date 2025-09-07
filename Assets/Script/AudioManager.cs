using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Instância estática para o padrão Singleton, permite acesso global.
    public static AudioManager instance;

    // Efeitos Sonoros (conectados via Inspector)
    [Header("Efeitos Sonoros")]
    public AudioClip somDeClique;
    public AudioClip somDeAcerto;
    public AudioClip somDeErro;
    public AudioClip somDeVitoria;
    public AudioClip somDeTransicao;

    // Músicas de Fundo (conectadas via Inspector)
    [Header("Música de Fundo")]
    public AudioClip musicaDaSplash;
    public AudioClip musicaDoMenu;
    public AudioClip musicaDoQuiz;

    // Gerenciador de Dicas (conectado via Inspector)
    [Header("Gerenciador de Dicas")]
    public List<SuculentaData> todasAsSuculentas;
    private List<string> dicasDisponiveis = new List<string>();

    // Componentes de áudio internos
    private AudioSource sfxSource;    // Para efeitos sonoros
    private AudioSource musicSource;  // Para música de fundo

    void Awake()
    {
        // Implementação do padrão Singleton para garantir uma única instância
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

        // Adiciona os componentes AudioSource via código
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; // Música de fundo deve repetir

        // Carrega as dicas das fichas de suculentas na memória
        ResetarDicasDisponiveis();
    }

    // Assina o evento de carregamento de cena quando o objeto é ativado
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Cancela a assinatura para evitar erros quando o objeto é desativado
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Função chamada automaticamente toda vez que uma nova cena é carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica o nome da cena e toca a música correspondente
        if (scene.name == "SplashScreen")
        {
            TocarMusica(musicaDaSplash);
        }
        else if (scene.name == "Quiz")
        {
            TocarMusica(musicaDoQuiz);
        }
        else // Para todas as outras cenas (Menu, Enciclopedia, etc.)
        {
            TocarMusica(musicaDoMenu);
        }
    }

    // Controla a reprodução da música de fundo
    public void TocarMusica(AudioClip musica)
    {
        // Evita reiniciar a música se ela já estiver tocando
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

    // Funções públicas para tocar efeitos sonoros específicos
    public void TocarSomClique() { if (somDeClique != null) sfxSource.PlayOneShot(somDeClique); }
    public void TocarSomAcerto() { if (somDeAcerto != null) sfxSource.PlayOneShot(somDeAcerto); }
    public void TocarSomErro() { if (somDeErro != null) sfxSource.PlayOneShot(somDeErro); }
    public void TocarSomVitoria() { if (somDeVitoria != null) sfxSource.PlayOneShot(somDeVitoria); }
    public void TocarSomTransicao() { if (somDeTransicao != null) sfxSource.PlayOneShot(somDeTransicao); }

    // Repopula a lista de dicas disponíveis com base nas fichas de suculentas
    public void ResetarDicasDisponiveis()
    {
        dicasDisponiveis.Clear();
        foreach (SuculentaData suculenta in todasAsSuculentas)
        {
            if (!string.IsNullOrEmpty(suculenta.dicaCuriosa))
            {
                dicasDisponiveis.Add(suculenta.dicaCuriosa);
            }
        }
    }

    // Sorteia e retorna uma dica única da lista de dicas disponíveis
    public string GetDicaAleatoria()
    {
        if (dicasDisponiveis.Count == 0)
        {
            ResetarDicasDisponiveis();
        }

        if (dicasDisponiveis.Count == 0)
        {
            return "Cadastre mais dicas curiosas nas fichas de suculentas!";
        }

        int indexAleatorio = Random.Range(0, dicasDisponiveis.Count);
        string dica = dicasDisponiveis[indexAleatorio];

        dicasDisponiveis.RemoveAt(indexAleatorio);

        return dica;
    }
}