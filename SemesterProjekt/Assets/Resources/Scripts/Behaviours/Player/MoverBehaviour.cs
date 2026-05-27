using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

// ReSharper disable once CheckNamespace
namespace Player
{
    public class MoverBehaviour : MonoBehaviour
    {
        #pragma warning disable
        
        //Unity variables
        private CapsuleCollider2D _capsuleCollider2D;
        private Rigidbody2D _rb;
        private Color _rayColor;
        private Vector2 _inputVector = Vector2.zero;
        private RaycastHit2D _raycastHitDown;
        public ParticleSystem Dust;
        private Animator _animator;
        

        // Regular variables
        private const float ExtraHeight = 0.5f;
        private readonly float _jumpForce = 12.0f;
        private float _inputX;
        private bool _canDoubleJump;
        private bool _wasGrounded, _isGrounded, _isWalking = false;

        private bool IsGrounded
        {
            get => _isGrounded;
            set
            {
                if (value != _isGrounded)
                {
                    _isGrounded = value;
                    if (_isGrounded && _rb.velocity.y <= 0.5)
                    {
                        LandingJuice();
                    }
                }
            }
        }


        // Serialized variables
        [SerializeField] private float moveSpeed = 10;
        
        [SerializeField] private int playerIndex;

        [SerializeField] private LayerMask whatIsGround;

        [SerializeField] private float squinches = 0.85f;
        private static readonly int Speed = Animator.StringToHash("Speed");


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        private void FixedUpdate()
        {
            MoveCharacter(_inputVector);
            _rb.gravityScale = 3f; //sets gravity scale to 3 times earths/normal gravity
        }

        private void Update()
        {
            DrawRays();
            FlipChar();
            CheckIfGrounded(_raycastHitDown);
        }

        public void SetInputVector(Vector2 direction)
        {
            _inputVector = direction;
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }


        private void MoveCharacter(Vector2 direction)
        {
            _rb.velocity = new Vector2(direction.x * moveSpeed, _rb.velocity.y);


            if (direction.x < 0)
            {
                _animator.SetFloat(Speed, _rb.velocity.x * -1f);
            }
            else
            {
                _animator.SetFloat(Speed, _rb.velocity.x);
            }
            
        }


        /// <summary>
        /// Method for checking whether or not the player is on the ground or not
        /// It uses a BoxCast instead of a RayCast since a BoxCast is better for a player since it also checks if the player is on an edge
        /// </summary>
        /// <returns>Returns true if the BoxCast hits a collider that is on the ground layer, else it returns false </returns>
        private void CheckIfGrounded(RaycastHit2D hit2D)
        {
            if (hit2D.collider != null)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        /// <summary>
        /// Method for making a BoxCast used for ground checking.
        /// It is also used to draw the BoxCast and changing the color to shot if it is true or false.
        /// </summary>
        private void DrawRays()
        {
            var bounds = _capsuleCollider2D.bounds;

            _raycastHitDown = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, ExtraHeight,
                whatIsGround);
            


            //If statement and color for showing if it is true or false
            _rayColor = _raycastHitDown.collider != null ? Color.green : Color.red;


            //DrawRay to show the BoxCast for ground check
            Debug.DrawRay(bounds.center + new Vector3(bounds.extents.x, 0),
                Vector2.down * (bounds.extents.y + ExtraHeight), _rayColor);
            Debug.DrawRay(bounds.center - new Vector3(bounds.extents.x, 0),
                Vector2.down * (bounds.extents.y + ExtraHeight), _rayColor);
            Debug.DrawRay(bounds.center - new Vector3(bounds.extents.x, bounds.extents.y + ExtraHeight),
                Vector2.right * (bounds.extents.x * 2), _rayColor);
        }

        //Jump method
        public void Jump(CallbackContext context)
        {
            // if BoxCast on the player is true aka they are on the ground, can double jump = true
            if (IsGrounded)
            {
                _canDoubleJump = true;
            }


            // if the jump button is pressed we run the if statement
            if (context.ReadValueAsButton())
            {
                // if the player is on the ground we jump if not we check if we can double jump
                if (IsGrounded)
                {
                    _rb.velocity = Vector2.up * _jumpForce;
                    CreateDust();
                }
                else
                {
                    if (context.ReadValueAsButton() && _canDoubleJump)
                    {
                        _rb.velocity = Vector2.up * _jumpForce;
                        CreateDust();
                        _canDoubleJump = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method for flipping/rotating the player model.
        /// </summary>
        private void FlipChar()
        {
            if (_rb.velocity.x > 0f)
            {
                
                transform.rotation = Quaternion.Euler(0f,0f,0f);
                CreateDust();
            }
            else if (_rb.velocity.x < 0f)
            {
                transform.rotation = Quaternion.Euler(0f,180f,0f);
                CreateDust();
            }
        }
        
        private void CreateDust()
        {
            Dust.Play();
        }
        

        private void LandingJuice()
        {
            const float time = 0.2f;
            var seq = DOTween.Sequence();

            seq.Append(transform.DOScaleY(squinches, time).SetEase(Ease.Linear))
                .Append(transform.DOScaleY(1, time).SetEase(Ease.Linear));
        }
    }
}