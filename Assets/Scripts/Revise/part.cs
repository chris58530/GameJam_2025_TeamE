using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Part", menuName = "Create Part")]
public class part : ScriptableObject
{
    public enum partType
    {
       Fusion,
       Discrete,
       Rejection
    }
    public partType type;
    public new string name;
}
