using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeObject : MonoBehaviour
{

    //only works for tmp pro 
    private TextMeshProUGUI tmpObj;

    public delegate void FadeComplete();
    public event FadeComplete onFadeComplete;

    public IEnumerator Fade(float fadeDuration, GameObject gObject)
    {
        TextMeshProUGUI rend = gObject.GetComponentInChildren<TextMeshProUGUI>();
        Color32 initialColor = rend.color;

        Color32 targetColor = new Color32(initialColor.r, initialColor.g, initialColor.b, 0);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            rend.color = Color32.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }
        if (onFadeComplete != null)
        {
            onFadeComplete();
        }
    }
}
