using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using TMPro;

public class RelayMessage : MonoBehaviour, MMEventListener<LoseAbilityEvent>
{
    public TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void OnMMEvent(LoseAbilityEvent abilityLost)
    {
        print("LoseAbilityEvent Triggered " + abilityLost.Ability);
        if (abilityLost.Ability == "Hover")
        {
            StartCoroutine(DisplayText("WARNING: Hover system failure.\nElevators now activated..."));
        }
    }

    public IEnumerator DisplayText(string textToDisplay)
    {
        text.text = textToDisplay;
        text.enabled = true;
        yield return new WaitForSeconds(5);
        text.enabled = false;
    }

    private void OnEnable()
    {
        this.MMEventStartListening<LoseAbilityEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<LoseAbilityEvent>();
    }
}
