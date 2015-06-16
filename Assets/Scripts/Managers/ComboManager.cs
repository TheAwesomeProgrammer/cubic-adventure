using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour {


    public Combo[] ComboList1;
    public Combo[] ComboList2;

    public int[] ComboPrice;

    public string[] ComboName;

    public string cCurrentComboName;
    public int cCurrentComboPoints;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int FindBestComboAndReturnPoints(Combo[] ComboIds1, Combo[] ComboIds2)
    {
        int tBestCompoPoint = 0;

        for (int i3 = 0; i3 < ComboIds1.Length; i3++)
        {
            for (int i4 = 0; i4 < ComboIds2.Length; i4++)
            {
                for (int i1 = 0; i1 < ComboList1.Length; i1++)
                {
                    for (int i2 = 0; i2 < ComboList1.Length; i2++)
                    {
                        if (i1 == i2 && i3 == i4)
                        {
                            
                            if (ComboList1[i1] == ComboIds1[i3] && ComboList2[i2] == ComboIds2[i4] && ComboPrice[i2] > tBestCompoPoint)
                            {
                                tBestCompoPoint = ComboPrice[i2];
                                cCurrentComboName = ComboName[i2];
                                cCurrentComboPoints = tBestCompoPoint;
                            }
                        }
                    }
                }                        
          }
        }

        if (tBestCompoPoint == 0)
        {
            tBestCompoPoint = 50;
            cCurrentComboPoints = tBestCompoPoint;
            cCurrentComboName = "No Combo";
        }



        return tBestCompoPoint;
    }
}
