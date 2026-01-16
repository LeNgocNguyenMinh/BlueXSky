using UnityEngine;

public class PlayerPlaneControlInLevelSelect : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField]private SpriteRenderer sr;

    private Vector2 lastTouchPos;
    private Camera cam;

    private float camWidth;
    private float camHeight;

    void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector2 delta = touch.position - lastTouchPos;
            Vector3 move = new Vector3(delta.x, delta.y, 0f) * moveSpeed;

            transform.position += move;
            ClampPosition();

            lastTouchPos = touch.position;
        }
    }

    void ClampPosition()
    {
        float halfW = sr.bounds.extents.x;
        float halfH = sr.bounds.extents.y;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -camWidth + halfW, camWidth - halfW);
        pos.y = Mathf.Clamp(pos.y, -camHeight + halfH, camHeight - halfH);

        transform.position = pos;
    }
}
