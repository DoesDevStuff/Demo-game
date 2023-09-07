using UnityEngine;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _doorCollider;

    [HideInInspector] public bool isBossRoomDoor = false;

    private BoxCollider2D _doorTrigger;
    private bool _isOpen = false;
    private bool _previouslyOpened = false;
    private Animator _animator;

    private void Awake()
    {
        _doorCollider.enabled = false;

        _animator = GetComponent<Animator>();
        _doorTrigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Settings.playerTag || collision.tag == Settings.playerWeapon)
        {
            OpenDoor();
        }
    }

    private void OnEnable()
    {
        _animator.SetBool(Settings.open, _isOpen);
    }

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            _isOpen = true;
            _previouslyOpened = true;
            _doorCollider.enabled = false;
            _doorTrigger.enabled = false;

            _animator.SetBool(Settings.open, true);

            // Sound manager here possibly?
        }
    }


    public void LockDoor()
    {
        _isOpen = false;
        _doorCollider.enabled = true;
        _doorTrigger.enabled = false;

        _animator.SetBool(Settings.open, false);
    }


    public void UnlockDoor()
    {
        _doorCollider.enabled = false;
        _doorTrigger.enabled = true;

        if (_previouslyOpened == true)
        {
            _isOpen = false;
            OpenDoor();
        }
    }
}