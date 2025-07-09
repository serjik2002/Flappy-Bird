using UnityEngine;

public class WaitState : State
{
    public WaitState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _player.SetStartPosition();
        _player.gameObject.SetActive(true);
    }

    public override void Exit()
    {

    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }
}



