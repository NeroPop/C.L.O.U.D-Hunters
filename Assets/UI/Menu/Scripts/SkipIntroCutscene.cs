using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroCutscene : MonoBehaviour
{
    private void Update()
    {
        
        
            SceneManager.LoadScene((int)SceneIndexes.Menu);
        
    }

    public void UIButtonPressed()
    {
        SceneManager.LoadScene((int)SceneIndexes.Menu);
    }
}
