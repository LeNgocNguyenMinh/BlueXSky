using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPlaneController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float yPos;
    private Vector3 target;
    public List<float> listX;
    private int currentIndex;
    private bool canMove = false;
    private void Start()
    {
        canMove = false;
        currentIndex = 0;
    }
    public void SetStartValue()
    {
        canMove = false;
        currentIndex = 0;
    }
    public void SetStartPosition()
    {
        listX = new List<float>();
        listX = BulletSpawnPoint.Instance.GetList4Point();
        transform.position = new Vector3(listX[0], yPos, transform.position.z);
        target = transform.position;
    }
    void Update()
    {
        if(!canMove)return;
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Ended:
                    if(InGamePauseManager.Instance.GetIsPause())return;
                    if(touch.position.x < Screen.width/2)
                        MoveLeft();
                    else
                        MoveRight();
                    break;
            }
        }
    }
    void MoveLeft()
    {
        if (currentIndex <= 0) return;

        currentIndex--;
        target = new Vector3(listX[currentIndex], yPos, transform.position.z);
    }

    void MoveRight()
    {
        if (currentIndex >= listX.Count - 1) return;

        currentIndex++;
        target = new Vector3(listX[currentIndex], yPos, transform.position.z);
    }
    void FixedUpdate()
    {
        if(!canMove)return;
        if(Vector3.Distance(rb.position, target) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.MovePosition(
            Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime)
        );
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
