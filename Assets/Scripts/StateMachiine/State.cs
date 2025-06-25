using UnityEngine;

public abstract class State
{
    protected readonly Player _player;
    protected readonly Rigidbody2D _rigidbody;

    public State(Player player)
    {
        _player = player;
        _rigidbody = player.GetComponent<Rigidbody2D>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void HandleInput() { }
}



