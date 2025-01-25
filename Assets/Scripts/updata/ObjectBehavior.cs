using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    private Brush spawner;
    private bool Isend;
    public void SetSpawner(Brush spawner)
    {
        this.spawner = spawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 假設地面標籤為 "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 通知生成器移除該物件
            spawner?.RemoveObjectFromList(this.gameObject);
            Destroy(this.gameObject); // 銷毀自身
        }
    }
}