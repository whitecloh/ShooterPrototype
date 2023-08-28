using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE && !UNITY_EDITOR)
                Application.Quit();
#endif
        }
    }
}
