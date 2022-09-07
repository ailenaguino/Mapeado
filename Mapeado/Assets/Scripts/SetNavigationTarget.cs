using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{

    /*[SerializeField]
    private Camera topDownCamera;*/
    [SerializeField]
    private TMP_Dropdown navigationTargetDropdown;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();
    [SerializeField]
    private GameObject indicator;

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;

    private bool lineToggle = true;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) {
            lineToggle = !lineToggle;
        }*/

        if (lineToggle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(indicator.transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            line.enabled = true;
        }
    }

    public void SetCurrentNavigationTarget(int selectedValue) {
        targetPosition = Vector3.zero;
        string selectedText = navigationTargetDropdown.options[selectedValue].text;
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.Equals(selectedText));
        if (currentTarget != null) {
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }
}
