using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        CameraManager.Instance.FindPlayer(gameObject);

        // 獲取 Rigidbody 組件
        rb = GetComponent<Rigidbody>();
        // 確保 Rigidbody 是使用物理運算
        rb.freezeRotation = true; // 防止因碰撞導致角色旋轉

    }

    void Update()
    {
        // 取得輸入方向
        float moveX = Input.GetAxis("Horizontal"); // A/D 或 左/右鍵
        float moveZ = Input.GetAxis("Vertical");   // W/S 或 上/下鍵

        // 設定移動向量
        movement = new Vector3(moveX, 0f, moveZ).normalized;
    }
    void FixedUpdate()
    {
       
        // 使用 Rigidbody 移動角色
        rb.MovePosition(rb.position + movement *  PlayerData.Instance.moveSpeed * Time.fixedDeltaTime);
    }
}
