using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _derection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {

        }
    }
}

