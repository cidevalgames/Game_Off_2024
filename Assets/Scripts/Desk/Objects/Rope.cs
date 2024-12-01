using Assets.Scripts.Animation;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Desk.Objects
{
    public class Rope : InteractableObject
    {
        [SerializeField] private Vector2 ropeVerticalBounds = new Vector2(-1, 1);

        public override void OnDragUpdate()
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 newPosition = new Vector2(cursorPos.x, cursorPos.y) - _mousePositionFromObject;

            float xPosition = Mathf.Clamp(newPosition.x, transform.position.x, transform.position.x);
            float yPosition = Mathf.Clamp(newPosition.y, _basePosition.y + ropeVerticalBounds.x, _basePosition.y + ropeVerticalBounds.y);
            
            transform.position = new Vector2 (xPosition, yPosition);
        }
    }
}