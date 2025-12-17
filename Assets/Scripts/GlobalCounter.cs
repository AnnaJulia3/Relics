using UnityEngine;

public class GlobalCounter : MonoBehaviour
{
  public static GlobalCounter Instance { get; private set; }

  public AudioClip victoryClip; // Arraste o arquivo de áudio aqui no Inspector

  private int count = 0;

  private GameObject canvasSuccess = null;
  private GameObject canvasFailed = null;
  private GameObject canvasFinish = null;

  private void Awake()
  {

    // esconde o canvas de fim de cena --- IGNORE ---
    canvasSuccess = GameObject.Find("CanvasSuccess");
    if (canvasSuccess != null) canvasSuccess.SetActive(false);

    canvasFailed = GameObject.Find("CanvasFailed");
    if (canvasFailed != null) canvasFailed.SetActive(false);

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
      Invoke("SceneCompleted", 1.5f);
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
          audioSource.Pause();
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
}
