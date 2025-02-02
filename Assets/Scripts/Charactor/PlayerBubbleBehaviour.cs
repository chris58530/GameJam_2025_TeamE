using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerBubbleBehaviour : MonoBehaviour
{
    public bool isTouchedOtherBubble = false;

    public List<OtherBubbleBehaviour> otherBubbleList = new List<OtherBubbleBehaviour>();

    bool isCarryOtherBubble = false;
    void OnEnable()
    {
        EventTable.onDestroyOtherBubble += KillBubble;
        EventTable.onPlayerWin += Win;
    }
    void OnDisable()
    {
        EventTable.onDestroyOtherBubble -= KillBubble;
        EventTable.onPlayerWin -= Win;
    }
    void Update()
    {
        OnTouchingOtherBubble();
    }

    void OnTouchingOtherBubble()
    {
        if (otherBubbleList.Count == 0) return;

        PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.TouchedBubbleSpeed);

        if (!Input.GetKeyDown(KeyCode.K)) return;

        var bubblesToRemove = new List<OtherBubbleBehaviour>();
        foreach (var otherBubble in otherBubbleList)
        {
            bubblesToRemove.Add(otherBubble);

            otherBubble.CollisionPlayer(this, false);

            PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.NormalSpeed);

        }
        foreach (var bubble in bubblesToRemove)
        {
            otherBubbleList.Remove(bubble);
        }

        AudioManager.current.PlayPushAudio();

    }
    void KillBubble(OtherBubbleBehaviour otherBubble)
    {
        foreach (var bubble in otherBubbleList)
        {
            bubble.CollisionPlayer(this, false);
        }
        otherBubbleList.Clear();
        PlayerData.Instance.SetMoveSpeed((int)PlayerData.PlayerState.NormalSpeed);

        PlayerData.Instance.AddScore(1);
        this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
    public void OnCollisionWeapon()
    {

        PlayerData.Instance.AddScore(-1);
        this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        CameraManager.Instance.ShakeCamera();
    }
    void Win()
    {
        StartCoroutine(WinAndFly());
    }
    IEnumerator WinAndFly()
    {
        StartCoroutine(loadScene());
        while (transform.position.y < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 100, transform.position.z), 0.25f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("gamewon");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<OtherBubbleBehaviour>(out var otherBubble))
        {
            if (otherBubble.IsTouchedPlayer) return;
            if (otherBubbleList.Contains(otherBubble)) return;
            otherBubbleList.Add(otherBubble);
            otherBubble.CollisionPlayer(this, true);

        }
    }


}
