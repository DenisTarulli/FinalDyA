using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private AnimationCurve animationCurve;
    private GraphicRaycaster raycaster;

    private void Start()
    {
        raycaster = GetComponentInChildren<GraphicRaycaster>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = animationCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        raycaster.enabled = false;
    }

    private IEnumerator FadeOut(string scene)
    {
        raycaster.enabled = true;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = animationCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
}
