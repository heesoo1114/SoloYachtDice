using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneContoller : MonoBehaviour
{
    public void LoadSceneFunc(string sceneName)
    {
        Debug.Log(sceneName);   
        SceneManager.LoadScene(sceneName);
    }
}
