using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;
    private void Awake() { instance = this; }

    public enum SpellElement { Fire, Water, Plant };
    public enum SpelZone { Line, Circle, Multiple };
    public enum SpellType { Impact, Bounce, Perforant };

    SpellElement actualElement = default;
    SpelZone actualZone = default;
    SpellType actualType = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpellParameter(Text text)
    {
        switch (text.text)
        {
            case ("Fire"):
                SetSpellParameter(SpellElement.Fire);
                break;
            case ("Plant"):
                SetSpellParameter(SpellElement.Plant);
                break;
            case ("Water"):
                SetSpellParameter(SpellElement.Water);
                break;

            case ("Circle"):
                SetSpellParameter(SpelZone.Circle);
                break;
            case ("Multi"):
                SetSpellParameter(SpelZone.Multiple);
                break;
            case ("Line"):
                SetSpellParameter(SpelZone.Line);
                break;

            case ("Impact"):
                SetSpellParameter(SpellType.Impact);
                break;
            case ("Perfo"):
                SetSpellParameter(SpellType.Perforant);
                break;
            case ("Bounce"):
                SetSpellParameter(SpellType.Bounce);
                break;
            default:
                break;
        }
    }
    public void SetSpellParameter(SpellElement parameter)
    {
        actualElement = parameter;
    }
    public void SetSpellParameter(SpelZone parameter)
    {
        actualZone = parameter;
    }
    public void SetSpellParameter(SpellType parameter)
    {
        actualType = parameter;
        Debug.Log(actualElement + " + " + actualZone + " + " + actualType);
    }
}
