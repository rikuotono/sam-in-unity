using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    public int canvas_number = 1;

    [NonSerialized] public int canvas_counter = 0;

    GameObject[] list_canvas;

    public StreamWriter sw;

    CanvasGroup canvasGroup;
    
    private GameObject _canvas_scale_change_to_2;

    // Start is called before the first frame update
    void Start()
    {
        list_canvas = new GameObject[canvas_number];

        var sampleData = "SampleText";
        CSVSave(sampleData, "sampleFile");

        for (int i = 0; i < canvas_number; i++)
        {
            list_canvas[i] = GameObject.Find("Canvas_" + i);
        }
        
        foreach (GameObject g in list_canvas)
        {
            g.SetActive(false);
        }
        
        Shuffle(list_canvas);

        list_canvas[canvas_counter].SetActive(true);
        canvasGroup = list_canvas[canvas_counter].GetComponent<CanvasGroup>();

        _canvas_scale_change_to_2 = GameObject.Find("Canvas_Scale_Change_to_2");
        
        _canvas_scale_change_to_2.SetActive(false);
    }
    
    void Shuffle(GameObject[] num) 
    {
        for (int i = 0; i < num.Length; i++)
        {
            //（説明１）現在の要素を預けておく
            GameObject temp = num[i]; 
            //（説明２）入れ替える先をランダムに選ぶ
            int randomIndex = UnityEngine.Random.Range(0, num.Length); 
            //（説明３）現在の要素に上書き
            num[i] = num[randomIndex]; 
            //（説明４）入れ替え元に預けておいた要素を与える
            num[randomIndex] = temp; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas_counter < canvas_number)
        {
            list_canvas[canvas_counter].SetActive(true);
            canvasGroup = list_canvas[canvas_counter].GetComponent<CanvasGroup>();
        }
            
        if (canvas_counter == canvas_number)
        {
            _canvas_scale_change_to_2.SetActive(true);
        }
    }

    private void CSVSave(string data, string fileName)
    {
        FileInfo fi;
        DateTime now = DateTime.Now;

        fileName = fileName + now.Year.ToString() + "_" + now.Month.ToString() + "_" + now.Day.ToString() + "__" + now.Hour.ToString() + "_" + now.Minute.ToString() + "_" + now.Second.ToString();
        fi = new FileInfo(Application.dataPath + "/AnswerData/" + fileName + ".csv");
        sw = fi.AppendText();
        string[] string_array = new string[] { "canvas_name", "valence_score", "arousal_score", "dominance_score" };
        sw.WriteLine(string.Join(",", string_array));
    }

    private void OnApplicationQuit()
    {
        sw.Flush();
        sw.Close();
        Debug.Log("Save Completed");
    }
}
