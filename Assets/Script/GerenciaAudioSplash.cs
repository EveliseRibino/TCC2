using UnityEngine;
using System.Collections; // Precisamos disso para a Corrotina

public class GerenciaAudioSplash : MonoBehaviour
{
    // Tempo de atraso em segundos antes de o som começar
    public float delayParaTocar = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // Pega a referência do Audio Source no mesmo objeto
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Inicia a rotina que vai esperar e depois tocar o som
        StartCoroutine(TocarSomComDelay());
    }

    IEnumerator TocarSomComDelay()
    {
        // Espera pelo tempo que definimos
        yield return new WaitForSeconds(delayParaTocar);

        // Só então, manda o Audio Source tocar
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}