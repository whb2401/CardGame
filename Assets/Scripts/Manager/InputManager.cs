using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> OnSwipeStarted;
    public static event Action<Vector3> OnSwipe;
    public static event Action<Vector3> OnSwipeEnded;

    private Vector3 startPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnSwipeStarted?.Invoke(startPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = startPosition - currentPosition;

            OnSwipe?.Invoke(movement);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = startPosition - currentPosition;

            OnSwipeEnded?.Invoke(movement);
        }
    }
}
