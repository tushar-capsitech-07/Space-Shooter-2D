using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickHandle;
    public RectTransform joystickBackground;
    public float moveSpeed = 1f; // Speed of the player
    public Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D

    private void Start()
    {
        joystickHandle.anchoredPosition = Vector2.zero; // Reset position
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        // Calculate the direction and clamp the magnitude
        Vector2 direction = position / (joystickBackground.sizeDelta / 2);
        direction = Vector2.ClampMagnitude(direction, 1);

        // Set the handle's position
        joystickHandle.anchoredPosition = direction * (joystickBackground.sizeDelta.x / 2);

        // Move the player based on the joystick direction
        MovePlayer(direction);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero; // Reset position on release
        MovePlayer(Vector2.zero); // Stop the player movement
    }

    private void MovePlayer(Vector2 direction)
    {
        // Calculate movement vector
        Vector2 moveDirection = direction * moveSpeed;

        // Move the player using Rigidbody2D
        playerRigidbody.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
    }
}