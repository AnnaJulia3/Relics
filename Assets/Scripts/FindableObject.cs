using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FindableObject : MonoBehaviour
{
  public TextMeshProUGUI targetText = null;
  public string objectTextID = "";
  public string objectImageID = "";

  [Header("Status")]
  [Tooltip("Indica se o objeto já foi encontrado")]
  private bool wasFound = false;

  public void FindObject()
  {
    Debug.Log("Clicado no objeto: " + this.GameObject().name);
    //   // Verifica se é a primeira vez que o objeto foi clicado
    //   if (wasFound)
    //   {
    //     return; // Já foi encontrado, não faz nada
    //   }

    // Marca como encontrado
    wasFound = true;

    //Pega o nome do objeto
    string nameObj = this.GameObject().name.Substring(2); // Remove os dois primeiros caracteres "ob"
    objectTextID = "textObj" + nameObj;

    objectImageID = "imgRef" + nameObj;

    targetText = GameObject.Find(objectTextID).GetComponent<TextMeshProUGUI>();

    // Sublinha o texto associado
    if (targetText != null) UnderlineText();
    else Debug.LogWarning($"Texto não configurado para o objeto: {this.GameObject().name}");

    // Feedback visual (opcional - pode ser expandido)
    OnObjectFound();

    Debug.Log($"Objeto encontrado: {this.GameObject().name}");

    //Desativa a interação com o botão, mantém imagem
    this.GameObject().GetComponent<UnityEngine.UI.Button>().interactable = false;
  }

  private void UnderlineText()
  {
    // Adiciona a tag de sublinhado do TextMesh Pro
    if (!targetText.text.Contains("<s>"))
    {
      targetText.text = $"<s>{targetText.text}</s>";
    }
  }

  // Método virtual para permitir comportamentos personalizados
  protected virtual void OnObjectFound()
  {
    // Pode adicionar efeitos visuais, animações, etc

    // Alterar a cor normal do botão para indicar que foi encontrado
    GameObject obj = GameObject.Find(objectImageID);

    if (obj != null)
    {
      // Acessa a imagem e alterar a cor pra FFFFFF
      UnityEngine.UI.Image img = obj.GetComponent<UnityEngine.UI.Image>();
      if (img != null)
      {
        img.color = Color.white; // Define a cor para branco
      }
    }
  }

  // Método público para verificar se já foi encontrado
  public bool IsFound()
  {
    return wasFound;
  }

  // Método para resetar o estado (útil para reiniciar o jogo)
  public void ResetObject()
  {
    wasFound = false;

    // Remove o sublinhado do texto
    if (targetText != null && targetText.text.Contains("<s>"))
    {
      targetText.text = targetText.text.Replace("<s>", "").Replace("</s>", "");
    }
  }
}
