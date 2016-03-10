using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{

    bool intTopView;
    public int scrollArea = 1;
    public int scrollSpeed = 1;
    public int dragSpeed = 1;
    Transform myTransform;

    // Use this for initialization
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mPosX = Input.mousePosition.x;
        float mPosY = Input.mousePosition.y;
        //var mouseposition = Camera.main.ScreenToWorldPoint(new Vector3(mPosX, mPosY, 0));
        //print(mouseposition);
        // Do camera movement by mouse position
        /*if (mPosX < scrollArea)
        {
            myTransform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
        }
        if (mPosX >= Screen.width - scrollArea)
        {
            myTransform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
        if (mPosY < scrollArea)
        {
            myTransform.Translate(Vector3.up * -scrollSpeed * Time.deltaTime);
        }
        if (mPosY >= Screen.height - scrollArea)
        {
            myTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }
		*/

        // Do camera movement by keyboard
        myTransform.Translate(new Vector3(Input.GetAxis("Horizontal") * scrollSpeed * Time.deltaTime, Input.GetAxis("Vertical") * scrollSpeed * Time.deltaTime, 0));

		/*
        // Do camera movement by holding down option                 or middle mouse button and then moving mouse
        if ((Input.GetKey("left alt") || Input.GetKey("right alt")) || Input.GetMouseButton(2))
        {
            myTransform.Translate(new Vector3(Input.GetAxis("Mouse X") * dragSpeed * -1, Input.GetAxis("Mouse Y") * dragSpeed * -1, 0));
        }



        //Scrolling Zoom
		if (Input.GetAxis("Mouse ScrollWheel") < -0) // forward
        {
            Camera.main.orthographicSize *= 1.1f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > -0) // back
        {
            Camera.main.orthographicSize *= 0.9f;
        }
		*/
		if (Input.GetKey("-") || Input.GetKey ("q")) //forward
		{
			Camera.main.orthographicSize *= 1.05f;
		}
		if (Input.GetKey("+") || Input.GetKey ("e")) //backward
		{
			Camera.main.orthographicSize *= 0.95f;
		}
    }
}

