using UnityEngine;
using System.Collections.Generic;



public class ComboWriter : MonoBehaviour {

    public GUIText GuiText;

    public int ComboOfName1;
    public int ComboOfName2;
    public int ComboOfName3;
    public int ComboOfName4;
    public int ComboOfName5;
    public int ComboOfName6;
    public int ComboOfName7;
    public int ComboOfName8;
    public int ComboOfName9;
    public int ComboOfName10;
    public int ComboOfName11;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        WriteComboNames();
	}

    public void AddComboName(string pComboName)
    {
        switch (pComboName)
        {
            case "Firey Love" :
                ComboOfName1++;
                break;
            case "Demon Love":
                ComboOfName2++;
                break;
            case "Love Birds":
                ComboOfName3++;
                break;
            case "Perfect Love":
                ComboOfName4++;
                break;
            case "Black Love":
                ComboOfName5++;
                break;
            case "Hard Love":
                ComboOfName6++;
                break;
            case "Gay Love":
                ComboOfName7++;
                break;
            case "Lesbian Love":
                ComboOfName8++;
                break;
            case "Taboo Love":
                ComboOfName9++;
                break;
            case "Forbidden Love":
                ComboOfName10++;
                break;
            case "No Combo":
                ComboOfName11++;
                break;


        }
    }

    void WriteComboNames()
    {
       

        string tCombo1 = "";
        string tCombo2 = "";
        string tCombo3 = "";
        string tCombo4 = "";
        string tCombo5 = "";
        string tCombo6 = "";
        string tCombo7 = "";
        string tCombo8 ="";
        string tCombo9 = "";
        string tCombo10 = "";
        string tCombo11 = "";

        if (ComboOfName1 > 0)
        {
            tCombo1 = "Firey Love : " + ComboOfName1 + "\n";
        }
        if (ComboOfName2 > 0)
        {
            tCombo2 = "Demon Love : " + ComboOfName2 + "\n";
        }
        if (ComboOfName3 > 0)
        {
            tCombo3 = "Love Birds : " + ComboOfName3 + "\n";
        }
        if (ComboOfName4 > 0)
        {
            tCombo4 = "Perfect Love : " + ComboOfName4 + "\n";
        }
        if (ComboOfName5 > 0)
        {
            tCombo5 = "Black Love : " + ComboOfName5 + "\n";
        }
        if (ComboOfName6 > 0)
        {
            tCombo6 = "Hard Love: " + ComboOfName6 + "\n";
        }
        if (ComboOfName7 > 0)
        {
            tCombo7 = "Gay Love : " + ComboOfName7 + "\n";
        }
        if (ComboOfName8 > 0)
        {
            tCombo8 = "Lesbian Love: " + ComboOfName8 + "\n";
        }
        if (ComboOfName9 > 0)
        {
            tCombo9 = "Taboo Love : " + ComboOfName9 + "\n";
        }
        if (ComboOfName10 > 0)
        {
            tCombo10 = "Forbidden Love : " + ComboOfName10 + "\n";
        }
        if (ComboOfName11 > 0)
        {
            tCombo11 =  "No Combo : " + ComboOfName11;
        }

        GuiText.GetComponent<GUIText>().text = "Combos : "+ "\n" +
            tCombo1 +
            tCombo2 +
            tCombo3 +
            tCombo4 +
            tCombo5 +
            tCombo6 +
            tCombo7 +
            tCombo8 +
            tCombo9 +
            tCombo10 +
            tCombo11;

    }

    
}
