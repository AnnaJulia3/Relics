using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
  public void load3()
  {
    SceneManager.LoadScene("Level3");
  }

  public void load5()
  {
    SceneManager.LoadScene("Level5");
  }


}
