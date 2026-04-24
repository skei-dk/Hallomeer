using UnityEngine;

public class FPSDisplayScript : MonoBehaviour
{
    float fps;
    public TMPro.TextMeshProUGUI FPSCounterText;

    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        FPSCounterText.text = "FPS: " + fps.ToString();
    }
}
