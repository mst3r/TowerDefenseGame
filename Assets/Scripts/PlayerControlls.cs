using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float dragSpeed = 0.1f;
    [SerializeField] private float edgeScrollSpeed = 10f;
    [SerializeField] private int edgeThickness = 10;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 20f;
    [SerializeField] private float maxZoom = 80f;

    [Header("Camera Bounds")]
    public bool useBounds = true;
    public Vector2 minBounds = new Vector2(-10, -10); //bottom left (x,y)
    public Vector2 maxBounds = new Vector2(10, 10);//top right (x-y)


    private Vector3 _lastMousePos;

    private IInteractable lastHovered;
    private IInteractable lastClicked;

    void Update()
    {
        HandleHover();
        HandleClick();
        CheckMovementInput();
        HandleKeyboardImput();
        HandleEdgeScroll();
        HandleZoom();
        ClampCameraToBounds();
    }

    private void HandleHover()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (lastHovered != interactable)
                {
                    // Exit old hover
                    if (lastHovered != null) lastHovered.OnHoverExit();

                    // Enter new hover
                    interactable.OnHoverEnter();
                    lastHovered = interactable;
                }
            }
        }
        else
        {
            // If nothing hit, clear hover
            if (lastHovered != null)
            {
                lastHovered.OnHoverExit();
                lastHovered = null;
            }
        }
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (lastHovered != null)
            {
                // deselect old clicked tile if different
                if (lastClicked != null && lastClicked != lastHovered)
                {
                    lastClicked.OnDeselected();
                }

                lastHovered.OnClick();
                lastClicked = lastHovered;
            }
        }
    }

    

    private void CheckMovementInput()
    {
        // When the left mouse button is pressed, capture the mouse position
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePos = Input.mousePosition;
        }

        // While the button is held, drag the camera
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _lastMousePos;

            // Move camera opposite to drag direction
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * 0.1f; // adjust speed with multiplier

            _camera.transform.Translate(move, Space.World);

            // Update last mouse position
            _lastMousePos = Input.mousePosition;
        }
    }
    private void HandleKeyboardImput()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;
        _camera.transform.Translate(move, Space.World);
    }

    private void HandleEdgeScroll()
    {
        Vector3 move = Vector3.zero;

        if(Input.mousePosition.x <= edgeThickness)
        {
            move.x -= 1;
        }
        else if (Input.mousePosition.x >= Screen.width - edgeThickness)
        {
            move.x += 1;
        }

        if (Input.mousePosition.z <= edgeThickness)
        {
            move.z -= 1;
        }
        else if (Input.mousePosition.z >= Screen.width - edgeThickness)
        {
            move.z += 1;
        }

        _camera.transform.Translate(move * edgeScrollSpeed * Time.deltaTime, Space.World);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(Mathf.Abs(scroll) > 0.01f)
        {
            float newSize = _camera.orthographic ?
            _camera.orthographicSize - scroll * zoomSpeed :
            _camera.fieldOfView - scroll * zoomSpeed;

            if (_camera.orthographic)
            {
                _camera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            }
            else
            {
                _camera.fieldOfView = Mathf.Clamp(newSize,minZoom, maxZoom);
            }

        }
    }

    private void ClampCameraToBounds()
    {
        if (!useBounds) return;

        Vector3 pos = _camera.transform.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);   
        pos.z = Mathf.Clamp(pos.z, minBounds.y, maxBounds.y);

        _camera.transform.position = pos;   
    }
}

