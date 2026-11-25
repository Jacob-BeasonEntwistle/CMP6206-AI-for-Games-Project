using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BillboardUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text typeText;

    public GameObject fsmPanel;
    public GameObject btPanel;
    public GameObject hivePanel;
    public GameObject flowerbedPanel;

    private Camera mainCamera;

    public enum ObjectType
    {
        FSM,
        BehaviourTree,
        Hive,
        Flowerbed
    }

    public ObjectType objectType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        fsmPanel.SetActive(false);
        btPanel.SetActive(false);
        hivePanel.SetActive(false);
        flowerbedPanel.SetActive(false);

        switch (objectType)
        {
            case ObjectType.FSM:
                fsmPanel.SetActive(true);
                typeText.text = "FSM";
                break;
            
            case ObjectType.BehaviourTree:
                btPanel.SetActive(true);
                typeText.text = "Behaviour Tree";
                break;

            case ObjectType.Hive:
                hivePanel.SetActive(true);
                typeText.text = "Environment";
                break;

            case ObjectType.Flowerbed:
                flowerbedPanel.SetActive(true);
                typeText.text = "Environment";
                break;
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

        transform.rotation = Quaternion.LookRotation(
            transform.position - mainCamera.transform.position);
    }
}
