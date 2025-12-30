using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int index;
    public void LoadLevel()
    {
        SceneManager.LoadScene(index);
    }
}
