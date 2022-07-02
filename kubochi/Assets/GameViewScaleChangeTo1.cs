using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameViewScaleChangeTo1 : MonoBehaviour
{
    GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = this.gameObject.transform.parent.gameObject;
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        Debug.Log(parentGameObject);  // ログを出力
        parentGameObject.SetActive(false);
        var asm = typeof(Editor).Assembly;
        var type = asm.GetType("UnityEditor.GameView");
        EditorWindow gameView = EditorWindow.GetWindow(type);

        // GameViewクラスのSnapZoomプライベートインスタンスメソッドを引数1で呼び出す
        var flag = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
        type.GetMethod("SnapZoom", flag, null, new System.Type[] {typeof(float)}, null)
            .Invoke(gameView, new object[] {1});
    }
}