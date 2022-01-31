using System;
using UnityEngine;

namespace Soulou
{
    public class EnemyRolling : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.5f;
        private Rigidbody2D _rigidbody;

        public Action<EnemyRolling> OnFinishTouched;
        public Action OnPlayerHit;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Ground)))
            {
                _rigidbody.AddForce(collision.transform.right * _speed, ForceMode2D.Impulse);
            }

            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(LiteralString.Mask_Finish)))
            {
                OnFinishTouched?.Invoke(this);
            }

            if (collision.gameObject.tag.Equals(LiteralString.Player))
            {
                OnPlayerHit?.Invoke();
            }
        }
    }
}
