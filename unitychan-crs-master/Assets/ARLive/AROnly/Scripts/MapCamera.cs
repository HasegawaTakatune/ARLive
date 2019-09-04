using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCamera : MonoBehaviour
{
    /// <summary>
    /// カメラComponent取得
    /// </summary>
    private Camera camera;

    /// <summary>
    /// RayCast用のマスク
    /// </summary>
    [SerializeField] private LayerMask mask;

    /// <summary>
    /// ARカメラの座標系を取得
    /// </summary>
    private Transform AROnlyCamera = null;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        camera = GetComponent<Camera>();
        StartCoroutine(SetAROnlyCamera());
    }

    /// <summary>
    /// AROnlyCameraの設定（取得できるまでループする）
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAROnlyCamera()
    {
        while (AROnlyCamera == null)
        {
            AROnlyCamera = GameObject.FindGameObjectWithTag("ARCamera").transform;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// <summary>
    /// マップ画面の表示切替
    /// </summary>
    public void SwitchDisplay()
    {
        // カメラが有効／無効で切り替える
        if (!camera.enabled)
            OpenMap();
        else
            CloseMap();
    }

    /// <summary>
    /// マップ画面を開く
    /// </summary>
    private void OpenMap()
    {
        camera.enabled = true;
#if UNITY_EDITOR
        Cursor.visible = true;
#endif
        // マップ画面からワープ場所を選択する
        StartCoroutine(SelectWarpPosition());
    }

    /// <summary>
    /// マップ画面を閉じる
    /// </summary>
    private void CloseMap()
    {
        camera.enabled = false;
#if UNITY_EDITOR
        Cursor.visible = false;
#endif
    }

    /// <summary>
    /// ワープ座標を選択する
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelectWarpPosition()
    {
        // AROnlyCameraが取得できていなければマップ画面を閉じる
        if (AROnlyCamera == null)
            CloseMap();

        // Unityエディタ・Android別にマップ選択処理をする
        while (camera.enabled)
        {
            yield return new WaitForSeconds(Time.deltaTime);
#if UNITY_EDITOR
            SelectWarpPoint_Click();
#elif UNITY_ANDROID
            SelectWarpPoint_Touch();
#endif
        }
    }

    /// <summary>
    /// ワープポイント選択（クリック）
    /// </summary>
    private void SelectWarpPoint_Click()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, mask))
            {
                AROnlyCamera.position = hit.point + Vector3.up;
            }
        }
    }

    /// <summary>
    /// ワープポイント選択（タッチ）
    /// </summary>
    private void SelectWarpPoint_Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (IsPointerOverUIObject(touch.position)) return;

                Ray ray = camera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, mask))
                {
                    AROnlyCamera.position = hit.point + Vector3.up;
                }
            }

        }
    }

    /// <summary>
    /// EventSystem.current.IsPointerOverGameObject(touch.fingerId)が
    /// うまく動かないらしいのでその代替え品、タッチした位置のオブジェクトを
    /// 全取得して、UIの存在確認をしている
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        EventSystem.current.RaycastAll(eventDataCurrentPosition, raycastResults);
        bool over = raycastResults.Count > 0;
        raycastResults.Clear();
        return over;
    }

}
