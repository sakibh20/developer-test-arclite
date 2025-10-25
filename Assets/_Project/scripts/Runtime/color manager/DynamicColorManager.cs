using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColorManager : MonoBehaviour
{
    private class ColorTarget
    {
        public Renderer Renderer;
        public SpriteRenderer SpriteRenderer;
        public List<Coroutine> ActiveCoroutines = new List<Coroutine>();
    }

    private static DynamicColorManager _instance;
    public static DynamicColorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("DynamicColorManager");
                _instance = go.AddComponent<DynamicColorManager>();
            }
            return _instance;
        }
    }

    private Dictionary<Object, ColorTarget> _targets = new Dictionary<Object, ColorTarget>();
    
    public void ChangeColor(Renderer renderer, Color targetColor, float duration)
    {
        if (renderer == null) return;

        if (!_targets.TryGetValue(renderer, out var target))
        {
            target = new ColorTarget { Renderer = renderer };
            _targets[renderer] = target;
        }

        // separate coroutine for each task
        Coroutine coroutine = StartCoroutine(ColorTransitionRenderer(target, targetColor, duration));
        target.ActiveCoroutines.Add(coroutine);
    }

    public void ChangeColor(SpriteRenderer spriteRenderer, Color targetColor, float duration)
    {
        if (spriteRenderer == null) return;

        if (!_targets.TryGetValue(spriteRenderer, out var target))
        {
            target = new ColorTarget { SpriteRenderer = spriteRenderer };
            _targets[spriteRenderer] = target;
        }

        Coroutine coroutine = StartCoroutine(ColorTransitionSprite(target, targetColor, duration));
        target.ActiveCoroutines.Add(coroutine);
    }

    private IEnumerator ColorTransitionRenderer(ColorTarget target, Color targetColor, float duration)
    {
        Renderer r = target.Renderer;
        if (r == null) yield break;

        Material mat = r.material;
        Color startColor = mat.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mat.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null;
        }

        mat.color = targetColor;
        target.ActiveCoroutines.RemoveAll(c => c == null);
    }

    private IEnumerator ColorTransitionSprite(ColorTarget target, Color targetColor, float duration)
    {
        SpriteRenderer sr = target.SpriteRenderer;
        if (sr == null) yield break;

        Color startColor = sr.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            sr.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null;
        }

        sr.color = targetColor;
        target.ActiveCoroutines.RemoveAll(c => c == null);
    }
}