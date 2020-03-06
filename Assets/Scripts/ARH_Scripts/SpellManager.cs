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

    [SerializeField] Transform spellLauncherM1;
    [SerializeField] Transform spellLauncherM2;
    [SerializeField] Transform spellLauncherM3;
    [SerializeField] bool isAiming = false;
    [SerializeField] GameObject spellPrefab = default;
    [SerializeField] float multipleLaunchDelay = 1.5f;
    [SerializeField] GameEvent onNextTurn;

    public Dictionary<Image, SpellElement> ElementsCurseurs = new Dictionary<Image, SpellElement>();
    public Dictionary<Image, SpellZone> ZoneCurseurs = new Dictionary<Image, SpellZone>();
    public Dictionary<Image, SpellType> TypeCurseurs = new Dictionary<Image, SpellType>();

    public Dictionary<Image, SpellElement> ElementsCroix = new Dictionary<Image, SpellElement>();
    public Dictionary<Image, SpellZone> ZoneCroix = new Dictionary<Image, SpellZone>();
    public Dictionary<Image, SpellType> TypeCroix = new Dictionary<Image, SpellType>();

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        SpellAiming();
    }

    public void ResetActual()
    {
        actualElement = SpellElement.ERROR;
        actualZone = SpellZone.ERROR;
        actualType = SpellType.ERROR;
    }

    public void SetAiming(bool state) => isAiming = state;
    void SpellAiming()
    {
        if (!isAiming) return;

        Vector2 pos = LaunchPosChecker();

        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = (mousePos - pos);

        if (ElementChecker() != SpellElement.ERROR || ZoneChecker() != SpellZone.ERROR || TypeChecker() != SpellType.ERROR)
            Debug.DrawRay(pos, pos + dir * 100, Color.blue);

        //Debug.Log(ElementChecker() + " + " + ZoneChecker() + " + " + TypeChecker());
        
        if (Input.GetMouseButtonDown(0) && (ElementChecker() != SpellElement.ERROR && ZoneChecker() != SpellZone.ERROR && TypeChecker() != SpellType.ERROR))
        {
            //Debug.Log("Spell launched");
            LaunchSpell();
            isAiming = false;
            StartCoroutine(RaiseNextTurnCoroutine());
        }
    }

    Vector2 LaunchPosChecker()
    {
        actualType = TypeChecker();
        Vector2 pos = Vector2.zero;

        switch (actualType)
        {

            case SpellType.Impact:
                pos = spellLauncherM1.position;
                break;
            case SpellType.Bounce:
                pos = spellLauncherM2.position;
                break;
            case SpellType.Perforant:
                pos = spellLauncherM3.position;
                break;
        }
        return pos;
    }

    public void LaunchSpell()
    {
        actualElement = ElementChecker();
        actualZone = ZoneChecker();
        actualType = TypeChecker();

        //Debug.Log("Spell launched");

        if (actualElement == SpellElement.ERROR || actualZone == SpellZone.ERROR || actualType == SpellType.ERROR)
            return;

        Vector2 pos = LaunchPosChecker();

        switch (actualType)
        {
            
            case SpellType.Impact:
                pos = spellLauncherM1.position;
                break;
            case SpellType.Bounce:
                pos = spellLauncherM2.position;
                break;
            case SpellType.Perforant:
                pos = spellLauncherM3.position;
                break;
            default:
                break;
        }

        //Debug.Log(ElementChecker() + " + " + ZoneChecker() + " + " + TypeChecker());

        Instantiate(spellPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        if (actualZone == SpellZone.Multiple)
        {
            StartCoroutine(SecondMultipleLaunch(pos));
        }
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

    public Image CroixFinder(SpellElement parameter)
    {
        foreach (KeyValuePair<Image, SpellElement> elementCroix in ElementsCroix)
        {
            if (parameter == elementCroix.Value)
            {
                return elementCroix.Key;
            }
        }
        return null;
    }
    public Image CroixFinder(SpellZone parameter)
    {
        foreach (KeyValuePair<Image, SpellZone> zoneCroix in ZoneCroix)
        {
            if (parameter == zoneCroix.Value)
            {
                return zoneCroix.Key;
            }
        }
        return null;
    }
    public Image CroixFinder(SpellType parameter)
    {
        foreach (KeyValuePair<Image, SpellType > typeCroix in TypeCroix)
        {
            if (parameter == typeCroix.Value)
            {
                return typeCroix.Key;
            }
        }
        return null;
    }

    public Button ButtonFinder(SpellElement parameter)
    {
        foreach (KeyValuePair<Image, SpellElement> elementCurseur in ElementsCurseurs)
        {
            if (parameter == elementCurseur.Value)
            {
                return elementCurseur.Key.gameObject.GetComponent<Button>();
            }
        }
        return null;
    }
    public Button ButtonFinder(SpellZone parameter)
    {
        foreach (KeyValuePair<Image, SpellZone> zoneCurseur in ZoneCurseurs)
        {
            if (parameter == zoneCurseur.Value)
            {
                return zoneCurseur.Key.gameObject.GetComponent<Button>();
            }
        }
        return null;
    }
    public Button ButtonFinder(SpellType parameter)
    {
        foreach (KeyValuePair<Image, SpellType> typeCurseur in TypeCurseurs)
        {
            if (parameter == typeCurseur.Value)
            {
                return typeCurseur.Key.gameObject.GetComponent<Button>();
            }
        }
        return null;
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

    IEnumerator SecondMultipleLaunch(Vector2 pos)
    {
        yield return new WaitForSeconds(multipleLaunchDelay);
        Instantiate(spellPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        StartCoroutine(ThirdMultipleLaunch(pos));
    }
    IEnumerator ThirdMultipleLaunch(Vector2 pos)
    {
        yield return new WaitForSeconds(multipleLaunchDelay);
        Instantiate(spellPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
    }
    IEnumerator RaiseNextTurnCoroutine()
    {
        yield return new WaitForSeconds(3);
        onNextTurn.Raise();
    }
}
