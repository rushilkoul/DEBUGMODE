using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int index;
    public void LoadLevelAtLocalIndex()
    {
        SceneManager.LoadScene(index);
    }
    public void LoadLevel(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}
