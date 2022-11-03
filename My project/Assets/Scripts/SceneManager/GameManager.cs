using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsMobileBrowser()
    {
#if UNITY_EDITOR
        return false; // value to return in Play Mode (in the editor)
#elif UNITY_WEBGL
    return WebGLHandler.IsMobileBrowser(); // value based on the current browser
#else
    return false; // value for builds other than WebGL
#endif
    }
    [SerializeField] private GameObject Joystick;
    [SerializeField] private GameObject JumpButton;
    [SerializeField] private Text nextText;
    [SerializeField] private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        if (IsMobileBrowser())
        {
            Joystick.SetActive(true);
            JumpButton.SetActive(true);
            controller.mobileOS = true;
            nextText.text = "[Next]";
        }
        else
        {
            controller.mobileOS = false;
        }

    }

}
