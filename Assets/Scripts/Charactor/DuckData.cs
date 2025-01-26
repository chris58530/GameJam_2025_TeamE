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
    public void ReSetHp()
    {
        StartCoroutine(ResetHp());
    }
    IEnumerator ResetHp()
    {
           yield return new WaitForSeconds(2f);

       while(hp<100)
       {
           hp++;
           EventTable.onDuckHealthChange?.Invoke(this.hp);
           yield return new WaitForSeconds(0.04f);
       }
    }
}
