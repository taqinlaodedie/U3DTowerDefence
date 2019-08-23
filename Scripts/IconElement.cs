using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    string towerName;
    Camera mainCamera;
    Transform atkRange; // Displayer of the attack range
    Material rangeMat;  // Material of the atteck range
    LayerMask layer;

    public void Init(Camera _mainCamera, Transform _atkRange)
    {
        towerName = GetComponent<Image>().sprite.name;  // Name of the image is the same with the tower
        mainCamera = _mainCamera;
        atkRange = _atkRange;
        rangeMat = atkRange.GetComponent<MeshRenderer>().material;
        layer = LayerMask.GetMask("Ground") | LayerMask.GetMask("Path") | LayerMask.GetMask("Platform");
    }

    Tower towerObj;
    // Click the image to initialize a tower
    public void OnPointerDown(PointerEventData eventData)
    {
        // Load a tower
        towerObj = Instantiate(Resources.Load<Tower>("Towers/" + towerName));
        // Display the attack range
        atkRange.gameObject.SetActive(true);
        atkRange.localScale = new Vector3(towerObj.attackRange * 2, 0.5f,towerObj.attackRange * 2);
        GetComponent<Image>().color = new Color(0, 1, 0);   // Change the image's color
    }

    bool canPlace;      // Check if the tower can be placed
    Transform terrain;  // Terrain to place
    // Place or delete a tower
    public void OnPointerUp(PointerEventData eventData)
    {
        if (canPlace)
        {
            // Place the tower on the terrain and become a child object
            towerObj.transform.position = terrain.position;
            towerObj.transform.SetParent(terrain);
            towerObj.Init();
        }
        else
            Destroy(towerObj.gameObject);
        atkRange.gameObject.SetActive(false);   // Stop displaying the attack range
        towerObj = null;
        GetComponent<Image>().color = new Color(1, 1, 1);
    }

    void Update()
    {
        // Find the place if a tower is instantiate
        if (towerObj != null)
        {
            // Send a ray from the main camera to the mouse
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, layer))
            {
                // The tower follows the position of the mouse
                towerObj.transform.position = hit.point;
                atkRange.position = hit.point;
                // Get the layer
                int index = hit.collider.gameObject.layer;
                // Tower can be placed if it's the right terrain and there's not a tower
                if (LayerMask.LayerToName(index) == "Platform" && hit.collider.transform.childCount == 0)
                {
                    canPlace = true;
                    terrain = hit.collider.transform;
                    rangeMat.color = new Color(0, 1, 0, 0.3f);
                }
                else
                {
                    canPlace = false;
                    rangeMat.color = new Color(1, 0, 0, 0.3f);
                }
            }
        }
    }
}
