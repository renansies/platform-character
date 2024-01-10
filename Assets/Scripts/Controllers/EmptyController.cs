using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyController : InputController
{
    public override float RetrieveMoveInput()
    {
        return 0f;
    }
    public override bool RetrieveJumpInput()
    {
        return false;
    }
    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
}
