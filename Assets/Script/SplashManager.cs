using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    // Tempo em segundos que a splash screen vai durar
    public float tempoDeDuracao = 20f;

    // O nome da cena para carregar
    public string nomeDaCena = "MenuPrincipal";

    // Uma "trava" para garantir que a cena só seja carregada uma vez
    private bool transicaoIniciada = false;

    void Start()
    {
        // Agenda a mudança automática de cena para o final do tempo
        Invoke("CarregarProximaCena", tempoDeDuracao);
    }

    // A função Update é chamada a cada frame
    void Update()
    {
        // Verifica se o usuário clicou com o mouse ou tocou na tela
        if (Input.GetMouseButtonDown(0))
        {
            // Se sim, chama a função para carregar a próxima cena imediatamente
            CarregarProximaCena();
        }
    }

    void CarregarProximaCena()
    {
        // Se a transição já começou (seja pelo toque ou pelo tempo), não faz mais nada.
        if (transicaoIniciada)
        {
            return;
        }

        // Ativa a trava para impedir chamadas futuras
        transicaoIniciada = true;

        // Cancela o agendamento do Invoke, caso o usuário tenha pulado
        CancelInvoke("CarregarProximaCena");

        // Carrega a cena do menu principal
        SceneManager.LoadScene(nomeDaCena);
    }
}