using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;

public class Editor : MonoBehaviour
{

    private RaycastHit hit;

    public GameObject wandImage;
    public GameObject wandEnd;

    public GameObject ARCam;
    public GameObject mainWorkspace;
    public GameObject mainImage;
    public GameObject phoneCam;

    public Button selectButton;
    public Button createButton;
    public Button deleteButton;

    public Button backButton;
    public Button createBackButton;
    public Button confirmButton;

    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;
    public Button optionsButton;

    public int toolMode = 0;
    //0 is main menu
    //1 is select
    //2 is create
    //3 is delete
    //4 is object selected
    //5 is translate

    public Text title;

    public GameObject editorMenu;
    public GameObject createMenu;
    public GameObject confirmMenu;
    public GameObject transformMenu;

    public GameObject playButton;
    public GameObject backToEditButton;

    public GameObject createObjectChooser;

    public Material sMat;

    public GameObject selectedObject;
    public Material[] selectedMats;

    public int objectNumber = 0;
    private ArrayList objects = new ArrayList();
    private Vector3 originalScale;
    private Transform originalTransform;
    private Vector3 relWandPos;
    private GameObject scalyObject;
    private Color originalColor;

    // Use this for initialization
    void Start()
    {

        title.text = "Editor Menu";
        editorMenu.SetActive(true);
        confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        createMenu.SetActive(false);
        createObjectChooser.SetActive(false);
        transformMenu.SetActive(false);
        backToEditButton.SetActive(false);

        selectButton.onClick.AddListener(delegate
        {
            selectPressed(selectButton);
        });

        createButton.onClick.AddListener(delegate
        {
            createPressed(createButton);
        });

        deleteButton.onClick.AddListener(delegate
        {
            deletePressed(deleteButton);
        });

        backButton.onClick.AddListener(delegate
        {
            backPressed(backButton);
        });

        createBackButton.onClick.AddListener(delegate
        {
            backPressed(createBackButton);
        });

        confirmButton.onClick.AddListener(delegate
        {
            confirmPressed(confirmButton);
        });

        translateButton.onClick.AddListener(delegate
        {
            translatePressed(translateButton);
        });

        rotateButton.onClick.AddListener(delegate
        {
            rotatePressed(rotateButton);
        });

        scaleButton.onClick.AddListener(delegate
        {
            scalePressed(scaleButton);
        });

        optionsButton.onClick.AddListener(delegate
        {
            optionsPressed(optionsButton);
        });
        playButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            playPressed();
        });
        backToEditButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            backToEditPressed();
        });

    }

    // Update is called once per frame
    void Update()
    {

        if (toolMode == 0 && playButton.activeSelf == false)
        {
            playButton.SetActive(true);
        }
        else if (toolMode != 0 && playButton.activeSelf == true)
        {
            playButton.SetActive(false);
        }

        //Vector3 camToImageDir = mainImage.transform.position + ARCam.transform.position;
        //camToImageDir.x += 26.8f;
        //Debug.Log(camToImageDir.x);
        if (mainImage.transform.localRotation.z > 0.1)
        {
            mainWorkspace.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        else if (mainImage.transform.localRotation.z < -0.1)
        {
            mainWorkspace.transform.Translate(0.01f, 0.0f, 0.0f);
        }

        //Debug.Log(mainImage.transform.localRotation.z);


        if (touchedAnObject())
        {
            Ray ray;

            if (Input.touchCount > 0)
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            else
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /*if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
            }*/
            if (Physics.Raycast(ray, out hit) && FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI") != null
                && FindParentWithName(hit.transform.gameObject, "EditorWorkspace") != null && toolMode == 1)
            {
                GameObject obstacle = FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI");
                selectObject(obstacle);

                title.text = "Confirm the object to select it (" + hit.transform.gameObject + " selected)";
                confirmButton.gameObject.SetActive(true);
            }
            else if (Physics.Raycast(ray, out hit) && FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI") != null
                && FindParentWithName(hit.transform.gameObject, "ObjectChooser") != null && toolMode == 2)
            {
                GameObject obj;
                GameObject obstacle = FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI");
                obj = (GameObject)Instantiate(obstacle, mainWorkspace.transform.position, Quaternion.identity);
                obj.name = obstacle.name + objectNumber;

                obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                obj.transform.parent = mainWorkspace.transform;

                /*if (obj.transform.Find("Group") != null)
                    obj.transform.Find("Group").position = ARCam.transform.position + ARCam.transform.forward * 1.3f;
                else*/
                obj.transform.position = ARCam.transform.position + ARCam.transform.forward * 1.3f;

                objectNumber++;
                //Debug.Log(obj);
                objects.Add(obj);

                backPressed(createBackButton);
                selectPressed(selectButton);
                selectObject(obj);
                confirmPressed(confirmButton);
            }
            else if (Physics.Raycast(ray, out hit) && FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI") != null
                && FindParentWithName(hit.transform.gameObject, "EditorWorkspace") != null && toolMode == 3)
            {
                GameObject obstacle = FindParentWithTag(hit.transform.gameObject, "Obstacle", "HasUI");
                selectObject(obstacle);

                title.text = "Confirm the object to delete it (" + hit.transform.gameObject + " selected)";
                confirmButton.gameObject.SetActive(true);
            }
        }

        if (toolMode == 2)
        {
            /*foreach (Transform child in createObjectChooser.GetComponentInChildren<Transform>())
                if (child.name != "Spotlight")
                    child.Rotate(new Vector3(0.0f, 0.5f, 0.0f));*/
        }
        else if (toolMode == 6) // rotate
        {
            if (wandImage.GetComponent<CustomTracker>().tracking == true)
            {
                selectedObject.transform.rotation = wandEnd.transform.rotation;
            }
        }
        else if (toolMode == 7) // scale
        {
            if (wandImage.GetComponent<CustomTracker>().tracking == true)
            {
                /*float dist;

                dist = Vector3.Distance(selectedObject.transform.position, wandEnd.transform.position);
                Vector3 direction = selectedObject.transform.position + wandEnd.transform.position;
                direction.Normalize();

                //Debug.Log(scaleFactor);
                if (dist < 20 && dist > 0.01)
                {
                    selectedObject.transform.localScale = (dist * 0.04f) * direction;//new Vector3(dist * dist * 0.1f, dist * dist * 0.1f, dist * dist * 0.1f);
                    selectedObject.transform.localScale = new Vector3(Mathf.Abs(selectedObject.transform.localScale.x), 
                        Mathf.Abs(selectedObject.transform.localScale.y), Mathf.Abs(selectedObject.transform.localScale.z));
                }
                else
                {
                    selectedObject.transform.localScale = originalScale;
                }*/

                relWandPos = selectedObject.transform.InverseTransformPoint(wandEnd.transform.position);
                //Debug.Log(relWandPos);

                scalyObject.transform.localScale = new Vector3(Mathf.Abs(relWandPos.x) * 0.2f, Mathf.Abs(relWandPos.y) * 0.2f, Mathf.Abs(relWandPos.z) * 0.2f);
            }

        }

    }

    public static GameObject FindParentWithName(GameObject childObject, string name)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.name == name)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag, string tag2)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag || t.parent.tag == tag2)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    bool IsPointerOverGameObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        return (eventSystem.IsPointerOverGameObject(fingerId)
            );
    }

    bool touchedAnObject()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            || (Input.mousePresent && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject());
    }

    void deSelect()
    {
        if (selectedObject != null && selectedMats != null)
        {
            revertObject(selectedObject, selectedMats);
            selectedObject = null;
            selectedMats = null;
        }
    }

    Material[] saveMats(GameObject p)
    {
        Transform[] c = p.GetComponentsInChildren<Transform>();
        int s = 0;
        foreach (Transform child in c)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                s = s + 1;
            }
        }
        Material[] m = new Material[s];
        int i = 0;
        foreach (Transform child in c)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                m[i] = child.GetComponent<Renderer>().material;
                i = i + 1;
            }
        }
        return m;
    }

    void revertObject(GameObject p, Material[] m)
    {
        Transform[] c = p.GetComponentsInChildren<Transform>();
        int i = 0;
        foreach (Transform child in c)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                //Debug.Log(m[i]);
                child.GetComponent<Renderer>().material = m[i];
                i = i + 1;
            }

        }
    }

    void selectObject(GameObject p)
    {
        selectedObject = p;
        selectedMats = saveMats(p);
        Transform[] c = p.GetComponentsInChildren<Transform>();
        foreach (Transform child in c)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                child.GetComponent<Renderer>().material = sMat;
            }
        }
    }

    void toggleObjectVisibility(GameObject p)
    {
        Transform[] c = p.GetComponentsInChildren<Transform>();
        foreach (Transform child in c)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = !child.GetComponent<MeshRenderer>().enabled;
            }
        }
    }

    public void backPressed(Button b)
    {
        if (toolMode == 1) // select
        {
            deSelect();
            toolMode = 0;
            editorMenu.SetActive(true);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            title.text = "Editor Menu";
        }
        else if (toolMode == 2) // create
        {
            deSelect();
            toolMode = 0;
            editorMenu.SetActive(true);
            createMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            createObjectChooser.SetActive(false);
            title.text = "Editor Menu";
        }
        else if (toolMode == 3) // delete
        {
            deSelect();
            toolMode = 0;
            editorMenu.SetActive(true);
            createMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            createObjectChooser.SetActive(false);
            title.text = "Editor Menu";
        }
        else if (toolMode == 4) // transform
        {
            deSelect();
            toolMode = 0;
            editorMenu.SetActive(true);
            createMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            createObjectChooser.SetActive(false);
            transformMenu.SetActive(false);
            title.text = "Editor Menu";
        }
    }

    public void confirmPressed(Button b)
    {
        if (toolMode == 1) // select
        {
            toolMode = 4;
            editorMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            transformMenu.SetActive(true);

            if (selectedObject.transform.Find("UI") != null)
            {
                optionsButton.gameObject.SetActive(true);
            }
            else
            {
                optionsButton.gameObject.SetActive(false);
            }
        }
        else if (toolMode == 3) // delete
        {
            objects.Remove(selectedObject);
            Destroy(selectedObject);
            backPressed(backButton);
        }
        else if (toolMode == 5) // translate
        {
            selectedObject.transform.parent = mainWorkspace.transform;
            toolMode = 4;
            editorMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            transformMenu.SetActive(true);
        }
        else if (toolMode == 6) // rotate
        {
            toolMode = 4;
            editorMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            transformMenu.SetActive(true);
        }
        else if (toolMode == 7) // scale
        {
            toolMode = 4;
            editorMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            transformMenu.SetActive(true);

            toggleObjectVisibility(selectedObject);

            selectedObject.transform.localScale = new Vector3(Mathf.Abs(relWandPos.x) * 0.2f, Mathf.Abs(relWandPos.y) * 0.2f, Mathf.Abs(relWandPos.z) * 0.2f);
            Destroy(scalyObject);
        }
        else if (toolMode == 8) // options
        {
            toolMode = 4;
            editorMenu.SetActive(false);
            //confirmMenu.SetActive(false);
            backButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            transformMenu.SetActive(true);

            selectedObject.SendMessage("hideUI");
        }
    }

    public void playPressed()
    {
        toolMode = -1;
        backToEditButton.SetActive(true);
        editorMenu.SetActive(false);
        //confirmMenu.SetActive(false);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        title.text = "Testing";

        phoneCam.GetComponent<EasyModeControl>().backtogame = true;
        phoneCam.GetComponent<EasyModeControl>().exitpressed = false;
    }

    public void backToEditPressed()
    {
        toolMode = 0;
        backToEditButton.SetActive(false);
        editorMenu.SetActive(true);
        //confirmMenu.SetActive(false);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        title.text = "Editor Menu";

        phoneCam.GetComponent<EasyModeControl>().Restart();
        phoneCam.GetComponent<EasyModeControl>().backtogame = false;
    }

    public void selectPressed(Button b)
    {
        toolMode = 1;
        editorMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(false);
        title.text = "Touch an object to select it";
    }

    public void deletePressed(Button b)
    {
        toolMode = 3;
        editorMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(false);
        title.text = "Touch an object to delete it";
    }

    public void createPressed(Button b)
    {
        toolMode = 2;
        editorMenu.SetActive(false);
        createMenu.SetActive(true);
        createObjectChooser.SetActive(true);
        title.text = "Touch an object to create it";
    }

    public void translatePressed(Button b)
    {
        toolMode = 5;
        transformMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
        selectedObject.transform.parent = ARCam.transform;
        title.text = "Move the camera to translate";
    }

    public void rotatePressed(Button b)
    {
        toolMode = 6;
        transformMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
        title.text = "Use the wand to rotate";
    }

    public void scalePressed(Button b)
    {
        toolMode = 7;
        transformMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);

        originalScale = selectedObject.transform.localScale;
        originalTransform = Instantiate(selectedObject.transform);

        title.text = "Use the wand to scale";

        scalyObject = (GameObject)Instantiate(selectedObject, selectedObject.transform.position, Quaternion.identity);
        scalyObject.name = "copy";
        scalyObject.transform.parent = mainWorkspace.transform;

        toggleObjectVisibility(selectedObject);

    }

    public void optionsPressed(Button b)
    {
        toolMode = 8;
        transformMenu.SetActive(false);
        //confirmMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);

        selectedObject.SendMessage("showUI");
        title.text = "Object options";
    }
}
