using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SpellManager : SerializedMonoBehaviour
{
    public static SpellManager instance;
    private void Awake() { instance = this; }

    public enum SpellElement { Fire, Water, Plant, ERROR };
    public enum SpellZone { Line, Circle, Multiple, ERROR };
    public enum SpellType { Impact, Bounce, Perforant, ERROR };

    public SpellElement actualElement = default;
    public SpellZone actualZone = default;
    public SpellType actualType = default;

    [SerializeField] Transform spellLauncher;
    [SerializeField] bool isAiming = true;
    [SerializeField] GameObject spellPrefab = default;

    public Dictionary<Image, SpellElement> ElementsCurseurs = new Dictionary<Image, SpellElement>();
    public Dictionary<Image, SpellZone> ZoneCurseurs = new Dictionary<Image, SpellZone>();
    public Dictionary<Image, SpellType> TypeCurseurs = new Dictionary<Image, SpellType>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpellAiming(spellLauncher.position);
    }

    void SpellAiming(Vector2 pos)
    {
        if (!isAiming) return;

        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = (mousePos - pos);
        Debug.DrawRay(pos, pos + dir * 100, Color.blue);
        Debug.Log(ElementChecker() + " + " + ZoneChecker() + " + " + TypeChecker());
        if (Input.GetMouseButtonDown(0) && (ElementChecker() != SpellElement.ERROR && ZoneChecker() != SpellZone.ERROR && TypeChecker() != SpellType.ERROR))
        {
            Debug.Log("Spell launched");
            LaunchSpell(pos);
            isAiming = false;
        }
    }

    void LaunchSpell(Vector2 pos)
    {
        actualElement = ElementChecker();
        actualZone = ZoneChecker();
        actualType = TypeChecker();

        Debug.Log(ElementChecker() + " + " + ZoneChecker() + " + " + TypeChecker());
        Instantiate(spellPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        isAiming = true;
    }

    SpellElement ElementChecker()
    {
        foreach (KeyValuePair<Image, SpellElement> elementCurseur in ElementsCurseurs)
        {
            if (elementCurseur.Key.enabled)
            {
                return elementCurseur.Value;
            }
        }
        return SpellElement.ERROR;
    }
    SpellZone ZoneChecker()
    {
        foreach (KeyValuePair<Image, SpellZone> zoneCurseur in ZoneCurseurs)
        {
            if (zoneCurseur.Key.enabled)
            {
                return zoneCurseur.Value;
            }
        }
        return SpellZone.ERROR;
    }
    SpellType TypeChecker()
    {
        foreach (KeyValuePair<Image, SpellType >  typeCurseur in TypeCurseurs)
        {
            if (typeCurseur.Key.enabled)
            {
                return typeCurseur.Value;
            }
        }
        return SpellType.ERROR;
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
                SetSpellParameter(SpellZone.Circle);
                break;
            case ("Multi"):
                SetSpellParameter(SpellZone.Multiple);
                break;
            case ("Line"):
                SetSpellParameter(SpellZone.Line);
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
    public void SetSpellParameter(SpellZone parameter)
    {
        actualZone = parameter;
    }
    public void SetSpellParameter(SpellType parameter)
    {
        actualType = parameter;
    }
}
