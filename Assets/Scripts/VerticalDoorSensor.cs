using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoorSensor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool isDoorOpen;
    [SerializeField] private float sensorRange;
    [SerializeField] private float doorSpeed;
    private float doorYPosition;
    [SerializeField] private LayerMask doorLayer;

    // Start is called before the first frame update
    void Start()
    {
        doorYPosition = door.transform.position.y + 3f;
    }

    // Update is called once per frame
    void Update()
    {
        isDoorOpen = Physics.CheckSphere(transform.position, sensorRange, doorLayer);

        if (isDoorOpen)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x, doorYPosition, door.transform.position.z), Time.deltaTime * doorSpeed);
        }
        else
        {
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x, doorYPosition - 3f, door.transform.position.z), Time.deltaTime * doorSpeed);
        }
    }
}
