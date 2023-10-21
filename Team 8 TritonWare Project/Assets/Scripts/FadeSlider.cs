using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSlider : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Fade Settings")]
    public float fadeMultiplier;
    private bool doFadeIn = false;
    private bool doFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOut() {
        if(canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime * fadeMultiplier;
        }
    }

    public void fadeIn() {
        if(canvasGroup.alpha < 1) {
            canvasGroup.alpha += Time.deltaTime * fadeMultiplier;
        }
    }
}
