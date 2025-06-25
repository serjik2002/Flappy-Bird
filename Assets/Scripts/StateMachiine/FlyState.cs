using UnityEngine;

public class FlyState : State
{
    public FlyState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Player is flying!");
    }

    public override void Update()
    {
        HandleInput();
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Jump();
        }
    }
}



