using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private Transform child;

    [SerializeField] private float speed = 0.1f;

    private Controller controller;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        controller = FindObjectOfType<Controller>();
        child = transform;
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        if (controller.Moving())
        {
            float angle = controller.GetAngle();
            parent.position += new Vector3(
                Mathf.Sin((child.localEulerAngles.y + angle + 90) * Mathf.PI / 180),
                0,
                -Mathf.Cos((child.localEulerAngles.y + angle + 90) * Mathf.PI / 180)) * speed;

            //Debug.Log(child.localEulerAngles.y);
        }
    }
}
