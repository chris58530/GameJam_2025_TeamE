using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EventTable
{

//回傳分數
    public static System.Action<OtherBubbleBehaviour> onDestroyOtherBubble;

    public static Action<int> onPlayerScoreChange;
    public static Action<int> onDuckHealthChange;
    public static Action onPlayerWin;
}
