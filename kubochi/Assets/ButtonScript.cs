using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
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
    }
}