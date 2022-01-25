using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    [CreateAssetMenu(fileName = "SpriteAnimatorData", menuName = "GameData/SpriteAnimatorData")]
    public class SpriteAnimatorData : ScriptableObject
    {
        [SerializeField]
        private List<SpriteSequence> _spriteSequences;// = new List<SpriteSequence>();

        public List<SpriteSequence> Sequences => _spriteSequences;

        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimationStatePlayer state;
            public float animationSpeed;
            public List<Sprite> sprites = new List<Sprite>(); 
        }
    }
}
