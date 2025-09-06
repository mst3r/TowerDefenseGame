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

    [Header("Camera Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform rotationTarget;

    [Header("Camera Bounds")]
    public bool useBounds = true;
    public Vector2 minBounds = new Vector2(-10, -10); //bottom left (x,y)
    public Vector2 maxBounds = new Vector2(10, 10);//top right (x-y)


    private Vector3 _lastMousePos;

    [Header("Defenders Placement")]
    public GameObject defenderPrefab;   // Assign turret prefab
    public LayerMask placementMask;     // What we can place on (e.g., ground layer)
    private TilesScript selectedTile;

    private IInteractable lastHovered;
    private IInteractable lastClicked;

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
        HandleHover();
        HandleClick();

        CheckMovementInput();
        HandleKeyboardImput();
        HandleEdgeScroll();
        HandleZoom();
        ClampCameraToBounds();
        HandleRotation();            
        }


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
        if (Input.GetMouseButtonUp(0) && lastHovered != null)
        {
            // If this tile is the same as the selected one, place defender
            if (selectedTile == lastHovered as TilesScript)
            {
                PlaceDefender(selectedTile);
                selectedTile.OnDeselected();
                selectedTile = null;
            }
            else
            {
                // Deselect previous tile
                if (selectedTile != null) selectedTile.OnDeselected();

                // Select new one
                selectedTile = lastHovered as TilesScript;
                if (selectedTile != null && selectedTile.isBuildable)
                {
                    selectedTile.OnClick();
                }
            }
        }
    }

    private void PlaceDefender(TilesScript tile)
    {
        if( tile != null && tile.isBuildable && defenderPrefab != null)
        {
            Vector3 placePos = tile.transform.position;
            placePos.y += 0.5f; //lift slightly above tile if needed

            Instantiate(defenderPrefab, placePos, Quaternion.identity );

            tile.isBuildable = false; //Prevent stacking
        }
    }

    //CAMERA STUFF BELOW

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

        if (Input.mousePosition.x <= edgeThickness)
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

        if (Mathf.Abs(scroll) > 0.01f)
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
                _camera.fieldOfView = Mathf.Clamp(newSize, minZoom, maxZoom);
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

    private void HandleRotation()
    {
        //Q/E keys to rotate around target
        if (rotationTarget != null)
        {
            if (Input.GetKey(KeyCode.Q))
                _camera.transform.RotateAround(rotationTarget.position, Vector3.up, rotationSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.E))
            {
                _camera.transform.RotateAround(rotationTarget.position, Vector3.up, -rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            //fallback: roate camera in place
            if (Input.GetKey(KeyCode.Q))
                _camera.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.E))
                _camera.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);

        }
    }
}

