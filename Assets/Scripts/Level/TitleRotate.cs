using UnityEngine;

public class TitleRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(
            (Input.mousePosition.x),
            (Input.mousePosition.y),
            (Input.mousePosition.z));
        mousePosition.z = Camera.main.nearClipPlane + 1;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        RotateTitle(mousePosition);
    }

    private void RotateTitle(Vector3 pointTo)
    {
        transform.LookAt(pointTo);
    }
}
