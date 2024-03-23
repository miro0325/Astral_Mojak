using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamMove : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float camSize;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float moveSpeed;
    private Vector3 startDragPos; 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraEdgeMoveHandler();
        CameraDragHandler();
        CameraZoomHandler();
    }

    private void CameraEdgeMoveHandler()
    {

    }

    private void CameraDragHandler()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            startDragPos = cam.ScreenToWorldPoint(mousePos);
            //Debug.Log(startDragPos);
        }

        if(Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 dist = startDragPos - cam.ScreenToWorldPoint(mousePos);
            dist.y = 0;
            cam.transform.position += dist * Time.deltaTime * moveSpeed;
            
        }
    }

    private void CameraZoomHandler()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camSize, Time.deltaTime * 10);
        camSize = Mathf.Clamp(camSize, minSize, maxSize);
        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(scroollWheel);
        camSize -= scroollWheel * Time.deltaTime * scrollSpeed;
       
    }
}
