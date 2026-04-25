using UnityEngine;
using UnityEngine.InputSystem;

public class IKTargetController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty leftAction;        // Buton stanga
    public InputActionProperty rightAction;       // Buton dreapta
    public InputActionProperty forwardAction;     // Buton fata
    public InputActionProperty backAction;        // Buton spate
    public InputActionProperty upAction;          // Buton sus
    public InputActionProperty downAction;        // Buton jos

    [Header("Speed")]
    public float moveSpeed = 0.5f;

    [Header("Boundaries")]
    public Vector3 minBounds = new Vector3(-1.5f, 1.0f, -1.5f);
    public Vector3 maxBounds = new Vector3( 1.5f, 3.0f,  1.5f);

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (leftAction.action != null)
            move.x -= leftAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (rightAction.action != null)
            move.x += rightAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (forwardAction.action != null)
            move.z += forwardAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (backAction.action != null)
            move.z -= backAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (upAction.action != null)
            move.y += upAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        if (downAction.action != null)
            move.y -= downAction.action.ReadValue<float>() * moveSpeed * Time.deltaTime;

        transform.Translate(move, Space.World);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z)
        );
    }
}