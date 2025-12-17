using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FindableObject : MonoBehaviour
{
  public TextMeshProUGUI targetText = null;
  public string objectTextID = "";
  public string objectImageID = "";

  public void FindObject()
  {
    // Pega o nome do objeto e os IDs associados
    // Remove os dois primeiros caracteres "ob"
    string nameObj = this.GameObject().name.Substring(2);
    objectTextID = "textObj" + nameObj;

    objectImageID = "imgRef" + nameObj;

    // Feedback: Marca o texto como encontrado
    UnderlineText();

    // Feedback visual 
    OnObjectFound();

    // Incrementa o contador global
    GlobalCounter.Instance.IncrementCount();

    //Desativa a interação, mantém imagem
    this.GameObject().GetComponent<UnityEngine.UI.Button>().interactable = false;
  }

  private void UnderlineText()
  {
    targetText = GameObject.Find(objectTextID).GetComponent<TextMeshProUGUI>();

    // Sublinha o texto associado
    if (targetText == null)
    {
      Debug.LogWarning($"Texto não configurado para o objeto: {this.GameObject().name}");
      return;
    }


    // Adiciona a tag de sublinhado do TextMesh Pro
    if (!targetText.text.Contains("<s>"))
    {
      targetText.text = $"<s>{targetText.text}</s>";
    }
  }

  // Método virtual para permitir comportamentos personalizados
  protected virtual void OnObjectFound()
  {
    // Alterar a base de cor normal para indicar que foi encontrado
    GameObject obj = GameObject.Find(objectImageID);

    if (obj != null)
    {
      // Acessa a imagem e alterar a cor pra FFFFFF
      UnityEngine.UI.Image img = obj.GetComponent<UnityEngine.UI.Image>();
      if (img != null)
      {
        img.color = Color.white;
      }
    }
  }

  // Método para resetar o estado (útil para reiniciar o jogo)
  //   public void ResetObject()
  //   {
  //     // Remove o sublinhado do texto
  //     if (targetText != null && targetText.text.Contains("<s>"))
  //     {
  //       targetText.text = targetText.text.Replace("<s>", "").Replace("</s>", "");
  //     }
  //   }

}
