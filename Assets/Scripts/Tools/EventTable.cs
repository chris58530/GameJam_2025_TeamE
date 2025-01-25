using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventTable
{

//回傳分數
    public static System.Action<OtherBubbleBehaviour> onDestroyOtherBubble;

    public static Action onPlayerScoreChange;
}
