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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_player.transform.position.y <= 6)
            {
                _player.Jump();
            }
        }
 #elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (_player.transform.position.y <= 6)
            {
                _player.Jump();
            }
        }
#endif
    }

        
}



