using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckData : Singleton<DuckData>
{
    public int hp = 100;
    public void SetHp(int hp)
    {
        this.hp -= hp;
        EventTable.onDuckHealthChange?.Invoke(this.hp);
    }
}
