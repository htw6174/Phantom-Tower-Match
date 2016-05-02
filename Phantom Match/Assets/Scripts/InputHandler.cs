using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public Vector3 mouseWorldPosition;

    public GridController gridController;

    void Update()
    {
        CheckClicks();
    }

    public Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    private void CheckClicks()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gridController.SelectBlock(GetMouseWorldPosition());
        }
        else if (Input.GetButton("Fire1"))
        {
            gridController.DragBlock(GetMouseWorldPosition());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            gridController.DeselectBlock();
        }
    }
}
