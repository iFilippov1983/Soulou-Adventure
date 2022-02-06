using System;
using UnityEngine;


namespace Soulou
{
    public class LevelObjectView : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private SpriteRenderer _spriteRenderer;
        private ScriptableObject _objectData;
        private SubjectState _state;

        public Transform Transform => _transform;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Collider2D Collider2D => _collider2D;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public ScriptableObject ObjectData { get { return _objectData; } set { _objectData = value; } }
        public SubjectState State { get { return _state; } set { _state = value; } }

        public Action<Collision2D> CollisionEnterEvent;
        public Action<Collider2D> TriggerEnterEvent;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _state = new SubjectState();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnterEvent?.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEnterEvent?.Invoke(collision);
        }
    }
}

