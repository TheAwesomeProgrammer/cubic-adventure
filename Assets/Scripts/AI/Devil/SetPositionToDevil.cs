using UnityEngine;
using System.Collections;

public class SetPositionToDevil : CupidBowSetPositionToPlayer {

    public Transform Devil;



    protected override void SetPosition()
    {
        GetComponent<Renderer>().enabled = true;
        Vector3 tWantedPosition = Devil.position + mOffset;

        transform.position = tWantedPosition;
    }
}
