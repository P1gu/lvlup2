using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
    public Animator startButton;
    public Animator quitButton;

    public void QuitMenu()
    {

        Application.Quit();
    }

    public void OpenSettings()
    {
        startButton.SetBool("isHidden", true);
        quitButton.SetBool("isHidden", true);
    }
}
