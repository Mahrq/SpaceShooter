using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Description:    Script attached to an object in the scene (preferably the eventSystem)
///                 Provides common Button UI Events such as on click events.
/// </summary>
public class ButtonUIEvents : MonoBehaviour
{  
    /// <summary>
    /// Input scene name onto the button string parameter on the Onclick event of the button.
    /// The method will load the scene
    /// </summary>
    public void OnClickSceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// Drag a UI gameobject into the parameter on the Onclick event of the button
    /// The method will get the state of the object and then reverse its state.
    /// </summary>
    public void OnClickToggleUIObject(GameObject uiObject)
    {
        bool currentObjectState = uiObject.activeInHierarchy;
        currentObjectState = !currentObjectState;
        uiObject.SetActive(currentObjectState);
    }
    /// <summary>
    /// The method will exit play mode when using the Unity  Editor but will close the application
    /// when running app.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(0);    
#endif
    }

}
