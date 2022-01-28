using UnityEngine;

namespace Soulou 
{
    public class LevelObjectModel
    {
        public Transform transform;
        public Rigidbody2D rigidbody2D;
        public Collider2D collider2D;
        public SpriteRenderer spriteRenderer;
        public ScriptableObject objectData;
        public SubjectState state;
    }
}
