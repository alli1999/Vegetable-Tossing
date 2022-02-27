using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVegetables : MonoBehaviour
{
    public string draggingTag;
    public Camera cam;
    private Vector3 distance;
    private float posX;
    private float posY;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidBody;
    private Vector3 previousPosition;

    void FixedUpdate()
    {
        if(Input.touchCount != 1)
        {
            dragging = false;
            touched = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if(touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if(Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag)
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidBody = toDrag.GetComponent<Rigidbody>();

                distance = cam.WorldToScreenPoint(previousPosition);
                posX = Input.GetTouch(0).position.x - distance.x;
                posY = Input.GetTouch(0).position.y - distance.y;

                SetDraggingProperties(toDragRigidBody);

                touched = true;
            }
        }

        if(touched && touch.phase == TouchPhase.Moved)
        {
            dragging = true;

            float posXNow = Input.GetTouch(0).position.x - posX;
            float posYNow = Input.GetTouch(0).position.y - posY;
            Vector3 curPos = new Vector3(posXNow, posYNow, distance.z);

            Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
            worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

            toDragRigidBody.velocity = worldPos / (Time.deltaTime * 10);

            previousPosition = toDrag.position;
        }

        if(dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            touched = false;
            previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
            SetFreeProperties(toDragRigidBody);
        }
    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.drag = 20;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.drag = 0;
    }
}
