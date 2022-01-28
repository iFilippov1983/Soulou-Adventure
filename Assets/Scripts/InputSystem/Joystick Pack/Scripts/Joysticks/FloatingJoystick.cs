using UnityEngine;
using UnityEngine.EventSystems;

namespace JoystickTools
{
    public sealed class FloatingJoystick : Joystick
    {
        private float _screenTapped;

        public float ScreenTapped => _screenTapped;
        protected override void Start()
        {
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
            FingerOnTheScreen(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
            FingerOnTheScreen(eventData);
        }

        internal override float GetHorizontal()
        {
            var axis = this.Horizontal;
            return axis;
        }

        internal override float GetVertical()
        {
            var axis = this.Vertical;
            return axis;
        }

        internal override Vector2 GetDirection()
        {
            var direction = this.Direction;
            return direction;
        }

        private void FingerOnTheScreen(PointerEventData eventData)
        {
            var fingerOnscreen = background.gameObject.activeInHierarchy;
            _screenTapped = fingerOnscreen ? 1 : 0;
        }
    }
}
