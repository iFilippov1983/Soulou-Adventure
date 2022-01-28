using System;
using System.Collections.Generic;
using UnityEngine;

namespace Soulou
{
    public class SpriteAnimatorController : IDisposable
    {
        private sealed class Animation
        {
            public SubjectState state;
            public List<Sprite> sprites;
            public bool isLooping;
            public bool isSleeping;
            public float speed = 1;
            public float counter;

            public void Execute()
            {
                if (isSleeping) return;
                counter += Time.deltaTime * speed;
                if (isLooping)
                {
                    while (counter > sprites.Count)
                    {
                        counter -= sprites.Count;
                    }
                }
                else if (counter > sprites.Count)
                {
                    counter = sprites.Count;
                    isSleeping = true;
                }
            }
        }

        private SpriteAnimatorData _spriteAnimatorData;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = 
            new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorController(SpriteAnimatorData data)
        {
            _spriteAnimatorData = data;
        }

        public void Execute()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Execute();
                if (animation.Value.counter < animation.Value.sprites.Count)
                {
                    animation.Key.sprite = animation.Value.sprites[(int)animation.Value.counter];
                }
            }
        }

        public void StartAnimation
            (
            SpriteRenderer spriteRenderer, 
            SubjectState animState, 
            bool isLooping, 
            float speed
            )
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.isSleeping = false;
                animation.isLooping = isLooping;
                animation.speed = speed;
                if (animation.state != animState)
                {
                    animation.state = animState;
                    animation.sprites = _spriteAnimatorData.Sequences.Find(sequense => sequense.state == animState).sprites;
                    //NB:Equals:
                    //Predicate<SpriteAnimatorData.SpriteSequence> predicate = (SpriteAnimatorData.SpriteSequence sequence) => { return sequence.state == animState; };
                    //animation.sprites = _spriteAnimatorData.Sequences.Find(predicate).sprites;
                    animation.counter = 0;
                }
            }
            else
            {
                var anim = new Animation();
                anim.isLooping = isLooping;
                anim.speed = speed;
                anim.state = animState;
                anim.sprites = _spriteAnimatorData.Sequences.Find(sequenses => sequenses.state == animState).sprites;
                _activeAnimations.Add(spriteRenderer, anim); 
            }
        }

        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimations.ContainsKey(spriteRenderer))
            {
                _activeAnimations.Remove(spriteRenderer);
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
