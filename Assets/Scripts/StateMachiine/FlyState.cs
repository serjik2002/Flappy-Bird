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
            if (_player.transform.position.y <= 6)
            {
                _player.Jump();
            }
        }
    }
}

public class DieState : State
{
    public DieState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        Debug.Log("Player is flying!");
    }
}



