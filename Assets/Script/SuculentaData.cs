using UnityEngine;

[CreateAssetMenu(fileName = "Nova Suculenta", menuName = "Suculenta/Criar Nova Suculenta")]
public class SuculentaData : ScriptableObject
{
    public string nome;
    public Sprite foto;
    [TextArea(3, 10)]
    public string descricao;
    public string iluminacao;
    public string rega;
    public string substrato;
}