using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoorSensor : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;
    [SerializeField] private bool isDoorOpen;
    [SerializeField] private float sensorRange;
    [SerializeField] private float doorSpeed;
    private float doorLeftZPosition;
    private float doorRightZPosition;
    [SerializeField] private LayerMask doorLayer;

    // Start is called before the first frame update
    void Start()
    {
        doorLeftZPosition = doorLeft.transform.position.z + 2f;
        doorRightZPosition = doorRight.transform.position.z - 2f;
    }

    // Update is called once per frame
    void Update()
    {
        isDoorOpen = Physics.CheckSphere(transform.position, sensorRange, doorLayer);

        if (isDoorOpen)
        {
            doorLeft.transform.position = Vector3.Lerp(doorLeft.transform.position, new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeftZPosition), Time.deltaTime * doorSpeed);
            doorRight.transform.position = Vector3.Lerp(doorRight.transform.position, new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, doorRightZPosition), Time.deltaTime * doorSpeed);
        }
        else
        {
            doorLeft.transform.position = Vector3.Lerp(doorLeft.transform.position, new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeftZPosition - 2f), Time.deltaTime * doorSpeed);
            doorRight.transform.position = Vector3.Lerp(doorRight.transform.position, new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, doorRightZPosition + 2f), Time.deltaTime * doorSpeed);
        }
    }
}
