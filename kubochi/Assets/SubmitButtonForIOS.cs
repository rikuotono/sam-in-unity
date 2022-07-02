using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.IO;

public class SubmitButtonForIOS : MonoBehaviour
{

    //連携するGameObject
    public ToggleGroup ios_toggleGroup;

    GameObject parentGameObject;
    // GameObject vivePointer;
    GameObject CanvasController;

    StreamWriter sw;

    // Use this for initialization
    void Start()
    {
        parentGameObject = this.gameObject.transform.parent.gameObject;
        // vivePointer = GameObject.Find("VivePointers");
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
        string selectedLabel_i = ios_toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;

        Debug.Log(selectedLabel_i);

        if (selectedLabel_i != "null")
        {
            sw = CanvasController.GetComponent<CanvasController>().sw;
            string question_name = parentGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.name;
            string[] id_array = new string[] { question_name, selectedLabel_i }; 
            sw.WriteLine(string.Join(",", id_array));
            parentGameObject.SetActive(false);
            // vivePointer.SetActive(false);
            int canvas_counter = CanvasController.GetComponent<CanvasController>().canvas_counter;
            CanvasController.GetComponent<CanvasController>().canvas_counter = canvas_counter + 1;
        }
    }
}