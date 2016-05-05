using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RotatingObstacle : MonoBehaviour
{

    private GameObject block;
    private GameObject ui;
    private GameObject canvas;

    private Editor editor;

    public Slider speedSlider;

    private int dir;

    public float speed = 10.0f;

    //private int lastSpeed = 10;
    //private int lastLength = 10;

    // Use this for initialization
    void Start()
    {
        block = transform.Find("Block").gameObject;
        ui = transform.Find("UI").gameObject;
        canvas = GameObject.Find("Canvas");
        editor = canvas.transform.Find("EditorMenu").transform.Find("EditorMenus").GetComponent<Editor>();

        //ui.transform.parent = canvas.transform;
        //ui.SetActive(false);

        speedSlider.onValueChanged.AddListener(delegate
        {
            SpeedValueChanged(speedSlider);
        });
    }

    private void SpeedValueChanged(Slider target)
    {
        speed = speedSlider.value;
    }

    // Update is called once per frame
    void Update()
    {

        /*if (lastLength != length)
        {
            guide.transform.localScale = new Vector3(length * 0.1f, 0.1f, 0.01f);
            lastLength = length;
        }*/
        Debug.Log(editor.toolMode);
        if (editor.toolMode != 6 && editor.toolMode != 7)
            block.transform.Rotate(new Vector3(speed * 0.2f, 0.0f, 0.0f));
    }

    public void showUI()
    {
        ui.transform.parent = canvas.transform;
        ui.transform.position = new Vector3(0, 0, 0);
        ui.transform.rotation = new Quaternion(0, 0, 0, 0);
        ui.transform.localScale = new Vector3(1, 1, 1);
    }
    public void hideUI()
    {
        ui.transform.parent = transform;
    }
}
