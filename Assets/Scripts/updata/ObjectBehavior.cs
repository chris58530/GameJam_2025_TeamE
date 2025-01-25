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
        // ���]�a�����Ҭ� "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �q���ͦ��������Ӫ���
            spawner?.RemoveObjectFromList(this.gameObject);
            Destroy(this.gameObject); // �P���ۨ�
        }
    }
}