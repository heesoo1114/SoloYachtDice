using UnityEngine;

public class IntroScene : MonoBehaviour
{
    private SceneContoller _sceneController;

    private void Awake()
    {
        _sceneController = GetComponent<SceneContoller>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneController.LoadSceneFunc("Main");
        }
    }
}
