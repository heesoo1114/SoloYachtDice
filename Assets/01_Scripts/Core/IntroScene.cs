using UnityEngine;

public class IntroScene : MonoBehaviour
{
    private SceneContoller _sceneManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneManager.LoadScene("Intro");
        }
    }
}
