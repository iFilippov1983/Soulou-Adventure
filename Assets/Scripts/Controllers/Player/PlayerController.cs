using UnityEngine;
using InputSystem;
using System;
using System.Collections;

namespace Soulou
{
    public sealed class PlayerController : IInitialization, IFixedExecute, IExecute, ICleanup
    {
        private LevelObjectViewModel _playerModel;
        private Rigidbody2D _playerRB;
        private Vector2 _finish;
        private Vector2 _moveDirection;
        private PlayerAnimator _animator;
        private Coroutine _deathPause;

        private IUserInputProxy _horizontalMovement;
        private IUserInputProxy _verticalMovement;
        private IUserInputProxy _jumpMovement;
        private float _horizontal;
        private float _vertical;
        private float _jump;

        private float _moveSpeed;
        private float _jumpStrength;

        private Collider2D[] _checkResults;
        private Vector2 _checkSize;
        private GameObject _hitObject;
        private float _ySizeCorrection = 0.2f;
        private float _xSizeCorrection = 2f;
        private float _checkPointCorrection = 0.5f;
        private int _resultsMaxAmount = 10;
        private bool _isGrounded;
        private bool _isClimbing;

        public Action OnFinishEnter;
        public Action OnPlayerDeath;

        public PlayerController(GameData gameData, LevelObjectViewModel playerModel, InputInitializer inputInitializer)
        {
            _playerModel = playerModel;
            _playerRB = _playerModel.rigidbody2D;
            _moveSpeed = gameData.PlayerData.MovementSpeed;
            _jumpStrength = gameData.PlayerData.JumpStrength;
            _finish = gameData.PlayerData.FinishPosition;

            _horizontalMovement = inputInitializer.GetInput().inputHorizontal;
            _verticalMovement = inputInitializer.GetInput().inputVertical;
            _jumpMovement = inputInitializer.GetInput().inputJump;
        }

        public void Initialize()
        {
            _checkResults = new Collider2D[_resultsMaxAmount];
            _animator = new PlayerAnimator(_playerModel);
            _playerModel.state = SubjectState.Idle;
            _animator.SetNewAnimation();

            _checkSize = _playerModel.collider2D.bounds.size;
            _checkSize.y += _ySizeCorrection;
            _checkSize.x /= _xSizeCorrection;

            _horizontalMovement.OnAxisChange += OnHorizontalAxisChange;
            _verticalMovement.OnAxisChange += OnVerticalAxisChange;
            _jumpMovement.OnAxisChange += OnJump;
        }

        public void FixedExecute()
        {
            _playerRB.MovePosition(_playerRB.position + _moveDirection * Time.fixedDeltaTime);
        }

        public void Execute(float deltaTime)
        {
            if (!_playerModel.state.Equals(SubjectState.Death))
            {
                CheckCollision();
                HandleMoving();
                SetState();
            }
            _animator.Execute();
        }

        public void Cleanup()
        {
            _horizontalMovement.OnAxisChange -= OnHorizontalAxisChange;
            _verticalMovement.OnAxisChange -= OnVerticalAxisChange;
            _jumpMovement.OnAxisChange -= OnJump;
        }

        public void OnPlayerHit()
        {
            DesactivatePlayer();
        }

        private void CheckCollision()
        {
            _isGrounded = false;
            _isClimbing = false;
            int amount = Physics2D.OverlapBoxNonAlloc(_playerModel.transform.position, _checkSize, 0f, _checkResults);

            for (int index = 0; index < amount; index++)
            {
                _hitObject = _checkResults[index].gameObject;
                if (_hitObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Ground)))
                {
                    _isGrounded = _hitObject.transform.position.y < (_playerRB.transform.position.y - _checkPointCorrection);
                    Physics2D.IgnoreCollision(_playerModel.collider2D, _checkResults[index], !_isGrounded);
                }
                else if (_hitObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Ladder)))
                {
                    _isClimbing = true;
                }
            }

            var finish = _playerModel.collider2D.OverlapPoint(_finish);
            if (finish)
            {
                _playerRB.gameObject.SetActive(false);
                OnFinishEnter?.Invoke();
            }
        }

        private void SetState()
        {
            if (_isClimbing && !_isGrounded)
            {
                _playerModel.state = SubjectState.Climb;
                _animator.SetNewAnimation();
                if (_moveDirection.y == 0) _animator.PauseAnimation();
                else _animator.UnpauseAnimation();
            }
            else if (_moveDirection.x == 0 && _isGrounded)
            {
                _playerModel.state = SubjectState.Idle;
                _animator.SetNewAnimation();
            }
            else if (_moveDirection.x != 0 && _isGrounded)
            {
                _playerModel.state = SubjectState.Run;
                _animator.SetNewAnimation();
            }
            else if (!_isGrounded)
            {
                _playerModel.state = SubjectState.Jump;
                _animator.SetNewAnimation();
            }
        }

        private void HandleMoving()
        {
            if (_isClimbing)
            {
                _moveDirection.y = _vertical * _moveSpeed;
            }
            else if (_jump > 0 && _isGrounded)
            {
                _moveDirection = Vector2.up * _jumpStrength;
            }
            else
            {
                _moveDirection += Physics2D.gravity * Time.deltaTime * 2;
            } 

            _moveDirection.x = _horizontal * _moveSpeed;

            if (_isGrounded)
            {
                _moveDirection.y = Mathf.Max(_moveDirection.y, -1f);
            }

            if (_moveDirection.x > 0)
            {
                _playerRB.transform.eulerAngles = Vector3.zero;
            }
            else if (_moveDirection.x < 0)
            {
                _playerRB.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            } 
        }

        private void OnVerticalAxisChange(float value)
        {
            _vertical = value;
        }

        private void OnHorizontalAxisChange(float value)
        {
            _horizontal = value;
        }

        private void OnJump(float value)
        {
            _jump = value;
        }

        private IEnumerator WaitForAnimationEnd()
        {
            yield return new WaitForSeconds(2f);
            DisablePlayer();
            OnPlayerDeath?.Invoke();
        }

        private void DesactivatePlayer()
        {
            _playerModel.state = SubjectState.Death;
            _animator.SetNewAnimation();
            _playerModel.collider2D.enabled = false;
            _playerRB.velocity = Vector3.zero;
            _deathPause = CoroutinesController.StartRoutine(WaitForAnimationEnd());
        }

        private void DisablePlayer()
        {
            CoroutinesController.StopRoutine(_deathPause);
            _playerModel.collider2D.enabled = true;
            _playerRB.gameObject.SetActive(false);
        }
    }
}
