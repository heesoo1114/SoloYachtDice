using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneContoller : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
