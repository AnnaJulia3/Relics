using UnityEngine;

public class GlobalCounter : MonoBehaviour
{
  public static GlobalCounter Instance { get; private set; }

  public AudioClip victoryClip; // Arraste o arquivo de áudio aqui no Inspector

  public AudioClip defeatClip; // Arraste o arquivo de áudio aqui no Inspector

  private int count = 0;

  private GameObject canvasSuccess = null;
  private GameObject canvasDefeat = null;
  private GameObject canvasFinish = null;

  private void Awake()
  {

    // esconde o canvas de fim de cena --- IGNORE ---
    canvasSuccess = GameObject.Find("CanvasSuccess");
    if (canvasSuccess != null) canvasSuccess.SetActive(false);

    canvasDefeat = GameObject.Find("CanvasDefeat");
    if (canvasDefeat != null) canvasDefeat.SetActive(false);

    canvasFinish = GameObject.Find("CanvasFinish");
    if (canvasFinish != null) canvasFinish.SetActive(false);

    // Implementa o padrão Singleton
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }

  }

  public void IncrementCount()
  {
    count++;
    if (count >= 7)
    {
      //espera 1 segundo antes de mostrar o canvas de cena completa
      Invoke("SceneCompleted", 1f);
    }
  }

  public int GetCount()
  {
    return count;
  }

  public void SceneCompleted()
  {
    // Mostra o canvas de cena completa
    if (canvasFinish != null) canvasFinish.SetActive(true);
    else Debug.LogWarning("CanvasFinish não encontrado!");

    if (canvasSuccess != null) canvasSuccess.SetActive(true);
    else Debug.LogWarning("CanvasSuccess não encontrado!");


    // Toca o som de vitória
    GameObject audioObj = GameObject.Find("AudioSource");
    if (audioObj != null)
    {
      AudioSource audioSource = audioObj.GetComponent<AudioSource>();
      if (audioSource != null)
      {
        if (audioSource.isPlaying)
        {
          Debug.Log("Parando AudioSource antes de tocar o som de vitória.");
          audioSource.Stop();
        }
        else
        {
          Debug.LogWarning("AudioSource não estava tocando!");
        }

        if (victoryClip != null)
        {
          audioSource.PlayOneShot(victoryClip);
        }
        else
        {
          Debug.LogWarning("Clip de vitória não encontrado! Arraste o arquivo no Inspector.");
        }
      }
    }
    else
    {
      Debug.LogWarning("AudioSource não encontrado!");
    }
  }

  public void SceneGameOver()
  {
    // Mostra o canvas de jogo perdido
    if (canvasFinish != null) canvasFinish.SetActive(true);
    else Debug.LogWarning("CanvasFinish não encontrado!");

    if (canvasDefeat != null) canvasDefeat.SetActive(true);
    else Debug.LogWarning("CanvasDefeat não encontrado!");


    // Toca o som de derrota
    GameObject audioObj = GameObject.Find("AudioSource");
    if (audioObj != null)
    {
      AudioSource audioSource = audioObj.GetComponent<AudioSource>();
      if (audioSource != null)
      {
        if (audioSource.isPlaying)
        {
          Debug.Log("Parando AudioSource antes de tocar o som de derrota.");
          audioSource.Stop();
        }
        else Debug.LogWarning("AudioSource não estava tocando!");

        if (defeatClip != null)
        {
          audioSource.PlayOneShot(defeatClip);
        }
        else
        {
          Debug.LogWarning("Clip de derrota não encontrado! Arraste o arquivo no Inspector.");
        }
      }
      else
      {
        Debug.LogWarning("AudioSource não encontrado!");
      }
    }
    else
    {
      Debug.LogWarning("AudioSource objeto não carregado!");
    }
  }
}
