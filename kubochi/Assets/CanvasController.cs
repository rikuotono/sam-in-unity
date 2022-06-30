using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    int canvas_number = 1;

    [SerializeField] private float interval = 1.0f;

    List<GameObject> list_canvas = new List<GameObject>();

    int canvas_counter = 0;
    float time_duration = 0.0f;

    // GameObject vivePointer;

    public StreamWriter sw;

    CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        // vivePointer = GameObject.Find("VivePointers");

        var sampleData = "SampleText";
        CSVSave(sampleData, "sampleFile");

        for (int i = 0; i < canvas_number; i++)
        {
            list_canvas.Add(GameObject.Find("Canvas_" + i));
        }
        
        foreach (GameObject g in list_canvas)
        {
            g.SetActive(false);
        }

        list_canvas[0].SetActive(true);
        canvasGroup = list_canvas[0].GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += 0.01f;
        }

        if (list_canvas[canvas_counter].activeSelf == false)
        {
            time_duration += Time.deltaTime;
        }

        if (time_duration > interval)
        {
            list_canvas[canvas_counter + 1].SetActive(true);
            // vivePointer.SetActive(true);
            canvas_counter += 1;
            time_duration = 0.0f;
            canvasGroup = list_canvas[canvas_counter].GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.00f;
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
