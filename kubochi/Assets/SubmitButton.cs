using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.IO;

public class SubmitButton : MonoBehaviour
{

    //連携するGameObject
    public ToggleGroup valence_toggleGroup;
    public ToggleGroup arousal_toggleGroup;
    public ToggleGroup dominance_toggleGroup;

    GameObject parentGameObject;
    GameObject CanvasController;

    StreamWriter sw;

    // Use this for initialization
    void Start()
    {
        parentGameObject = this.gameObject.transform.parent.gameObject;
        CanvasController = GameObject.Find("CanvasController");
        sw = CanvasController.GetComponent<CanvasController>().sw;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void onClick()
    {
        //Get the label in activated toggles
        string selectedLabel_v = valence_toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;

        string selectedLabel_a = arousal_toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;


        string selectedLabel_d = dominance_toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;

        Debug.Log(selectedLabel_v + selectedLabel_a + selectedLabel_d);

        if (selectedLabel_v != "null" && selectedLabel_a != "null" && selectedLabel_d != "null")
        {
            sw = CanvasController.GetComponent<CanvasController>().sw;
            string question_name = parentGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.name;
            string[] id_array = new string[] { question_name, selectedLabel_v, selectedLabel_a, selectedLabel_d }; 
            sw.WriteLine(string.Join(",", id_array));
            parentGameObject.SetActive(false);
            int canvas_counter = CanvasController.GetComponent<CanvasController>().canvas_counter;
            CanvasController.GetComponent<CanvasController>().canvas_counter = canvas_counter + 1;
        }
    }
}