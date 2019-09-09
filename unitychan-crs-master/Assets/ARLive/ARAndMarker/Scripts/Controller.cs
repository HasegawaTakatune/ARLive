using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private RectTransform StartController;
    [SerializeField] private RectTransform MoveController;

    private Vector3 startPosition;
    [SerializeField] private float range = 5;

    private float angle;
    private bool move = false;

    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR
        MouseControll();
#elif UNITY_ANDROID
        TouchControll();
#endif
    }

    private void MouseControll()
    {
        if (Input.GetMouseButtonDown(0))
            BeginControll(Input.mousePosition);
        if (Input.GetMouseButton(0))
            MoveControll(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
            EndControll();
    }

    private void TouchControll()
    {

    }

    private void BeginControll(Vector3 pos)
    {
        StartController.gameObject.SetActive(true);
        MoveController.gameObject.SetActive(true);

        StartController.position = pos;
        MoveController.position = pos;

        startPosition = pos;
    }

    private void EndControll()
    {
        move = false;

        StartController.gameObject.SetActive(false);
        MoveController.gameObject.SetActive(false);
    }

    private void MoveControll(Vector3 pos)
    {
        move = true;

        MoveController.position = GetRange(pos);

        SetAngle();
    }

    private Vector3 GetRange(Vector3 pos)
    {
        return new Vector3(
            Mathf.Clamp(pos.x, startPosition.x - range, startPosition.x + range),
            Mathf.Clamp(pos.y, startPosition.y - range, startPosition.y + range),
            Mathf.Clamp(pos.z, startPosition.z - range, startPosition.z + range));
    }

    private float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    private void SetAngle()
    {
        angle = GetAim(MoveController.position, startPosition) + 180;
        Debug.Log(angle);
        //float x = MoveController.position.x - startPosition.x;
        //float y = MoveController.position.y - startPosition.y;
        //angle = new Vector2(Mathf.Atan2(x, y), Mathf.Atan2(y, x));
    }

    public float GetAngle() { return angle; }

    public bool Moving() { return move; }
}
