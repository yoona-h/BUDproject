using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial_manager : MonoBehaviour
{
    [Header("Scene")]
    public string StartScene;

    public void End_Tutorial()
    {
        GameData.Instance.FirstGame = false;
        GameData.Instance.SaveData();
        SceneManager.LoadScene(StartScene);
    }
}