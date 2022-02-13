using UnityEngine;

namespace Soulou
{
    public class BallHandler
    {
        private const int _resultsMaxAmount = 10;
        private EnemyCannonBall _ballView;
        private Collider2D[] _checkResults;
        private GameObject _hitObject;
        private bool _isGrounded;

        public BallHandler(EnemyCannonBall ballView)
        {
            _ballView = ballView;
            _ballView.gameObject.SetActive(false);
            _checkResults = new Collider2D[_resultsMaxAmount];
        }

        public void Execute()
        {
            CheckCollision();
        }

        public void Throw(Transform transform, Vector3 velocity)
        {
            _ballView.gameObject.SetActive(true);
            _ballView.Rigidbody2D.velocity = Vector2.zero;
            _ballView.Transform.position = transform.position;
            _ballView.Transform.rotation = transform.rotation;
            _ballView.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }

        public void Cleanup()
        {
            if(_ballView) Object.Destroy(_ballView.gameObject);
        }

        private void CheckCollision()
        {
            int amount = Physics2D.OverlapBoxNonAlloc
                (
                _ballView.Transform.position,
                _ballView.Collider2D.bounds.size * 2f,
                0f, 
                _checkResults
                );

            for (int index = 0; index < amount; index++)
            {
                _hitObject = _checkResults[index].gameObject;
                if (_hitObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Ground)))
                {
                    var checkPointCorrection = _ballView.Collider2D.bounds.size.y;
                    _isGrounded = _hitObject.transform.position.y < (_ballView.Transform.position.y - checkPointCorrection);
                    Physics2D.IgnoreCollision(_ballView.Collider2D, _checkResults[index], !_isGrounded);
                }
            }
        }
    }
}
