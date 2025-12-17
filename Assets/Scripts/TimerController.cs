using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
  [Header("Configurações do Temporizador")]
  [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private float timerDuration = 180f; // 3 minutos em segundos

  private float currentTime;
  private bool isTimerRunning = false;

  private void Start()
  {
    currentTime = timerDuration;

    UpdateTimerDisplay();
    isTimerRunning = true;
  }

  private void Update()
  {
    if (currentTime > 0)
    {
      UpdateTimerDisplay();
      currentTime -= Time.deltaTime;

      if (currentTime <= 0)
      {
        currentTime = 0;
        OnTimerFinished();
      }
    }
  }

  /// <summary>
  /// Atualiza o texto do temporizador com o formato MM:SS
  /// </summary>
  private void UpdateTimerDisplay()
  {
    int minutes = Mathf.FloorToInt(currentTime / 60f);
    int seconds = Mathf.FloorToInt(currentTime % 60);

    if (timerText != null)
    {
      timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
  }

  /// <summary>
  /// Inicia o temporizador
  /// </summary>
  public void StartTimer()
  {
    isTimerRunning = true;
  }

  /// <summary>
  /// Pausa o temporizador
  /// </summary>
  public void PauseTimer()
  {
    isTimerRunning = false;
  }

  /// <summary>
  /// Reinicia o temporizador
  /// </summary>
  public void ResetTimer()
  {
    currentTime = timerDuration;
    isTimerRunning = false;
    UpdateTimerDisplay();
  }

  /// <summary>
  /// Chamado quando o temporizador termina
  /// </summary>
  private void OnTimerFinished()
  {
    isTimerRunning = false;
    Debug.Log("Temporizador finalizado!");
    // Adicione aqui a lógica do que acontece quando o tempo acaba
  }

  /// <summary>
  /// Retorna o tempo restante em segundos
  /// </summary>
  public float GetRemainingTime()
  {
    return currentTime;
  }

  /// <summary>
  /// Verifica se o temporizador está rodando
  /// </summary>
  public bool IsRunning()
  {
    return isTimerRunning;
  }
}
