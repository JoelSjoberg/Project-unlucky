using UnityEngine;

public class TimeController : MonoBehaviour {
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 0.5f;

    private void Update()
    {
        Time.timeScale += (1 / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void slowDown(float length)
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        slowDownLength = length;
    }
}
