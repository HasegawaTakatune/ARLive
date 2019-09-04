using UnityEngine;

public class ShowMap : MonoBehaviour
{
    /// <summary>
    /// マップカメラ取得
    /// </summary>
    [SerializeField] private MapCamera mapCamera; 

    /// <summary>
    /// マップ表示・非表示切り替え
    /// </summary>
    public void OnClickShowMapButton()
    {
        mapCamera.SwitchDisplay();
    }
}
