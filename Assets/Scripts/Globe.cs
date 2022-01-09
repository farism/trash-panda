using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    public UI ui;
    public GameObject location1;
    public GameObject location2;
    public GameObject location3;
    public GameObject location4;
    public GameObject location5;

    GameObject[] locations;

    void Awake()
    {
        locations = new GameObject[] { location1, location2, location3, location4, location5 };
    }

    void Update()
    {
        UpdateGlobeTransform();

        UpdateJobLocations();
    }

    void UpdateGlobeTransform()
    {
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * 10;
            float rotY = Input.GetAxis("Mouse Y") * 10;
            transform.rotation = Quaternion.Euler(rotY, -rotX, 0) * transform.rotation;
        }

        transform.localScale += new Vector3(1, 1, 1) * Input.mouseScrollDelta.y * Time.deltaTime * 30;
    }

    void UpdateJobLocations()
    {
        for (var i = 0; i < locations.Length; i++)
        {
            if (locations[i].transform.position.z < 0)
            {
                ui.ShowJobLocation(i, locations[i].transform.position);
            }
            else
            {
                ui.HideJobLocation(i);
            }
        }
    }

    Vector3 PolarToCartesian(Vector2 polar)
    {
        var origin = new Vector3(0, 0, 1);
        //build a quaternion using euler angles for lat,lon
        var rotation = Quaternion.Euler(polar.x, polar.y, 0);
        //transform our reference vector by the rotation. Easy-peasy!
        var point = rotation * origin * 3;

        return point;
    }
}
