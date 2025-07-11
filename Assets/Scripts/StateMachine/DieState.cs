using UnityEngine;

public class DieState : State
{
    public DieState(Player player) : base(player) { }

    public override void Enter()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        Debug.Log("Player is flying!");
    }
}



