using System;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(fileName = "SpriteAnimatorData", menuName = "GameData/SpriteAnimatorData")]
    public class SpriteAnimatorData : ScriptableObject
    {
        [SerializeField]
        private List<SpriteSequence> _spriteSequences;

        public List<SpriteSequence> Sequences => _spriteSequences;

        [Serializable]
        public sealed class SpriteSequence
        {
            public SubjectState state;
            public float animationSpeed;
            public bool isLooping;
            public List<Sprite> sprites = new List<Sprite>(); 
        }
    }
}
