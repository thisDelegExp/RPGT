using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;
    [SerializeField]
    private Text spellName;
    [SerializeField]
    private Image icon;
    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;
    [SerializeField]
    private Text castTime;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Spell[] spells;


    public Spell PrepareSpell(int index)
    {
        castingBar.fillAmount = 0;
        castingBar.color = spells[index].GetBarColor;
        spellName.text = spells[index].GetName;
        icon.sprite = spells[index].GetIcon;
        spellRoutine=StartCoroutine(Progress(index));
        fadeRoutine = StartCoroutine(FadeBar());
        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / spells[index].GetCastTime;
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0.0f, 1.0f,progress);
            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;
            castTime.text = (spells[index].GetCastTime - timePassed).ToString("F2");
            if (spells[index].GetCastTime - timePassed < 0)
                castTime.text = "0.0";
            yield return null;
        }
        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        
        float rate = 1.0f / 0.25f;
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }
}
