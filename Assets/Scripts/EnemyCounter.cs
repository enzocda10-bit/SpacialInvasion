using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyCount;

    private HorizontalDoorSensor doorComponent;

    private void Awake()
    {
        doorComponent = GetComponent<HorizontalDoorSensor>();
    }

    private void Update()
    {
        bool todosDerrotados = TodosOsInimigosForamDerrotados();

        doorComponent.enabled = todosDerrotados;
    }

    private bool TodosOsInimigosForamDerrotados()
    {
        foreach (GameObject enemy in enemyCount)
        {
            // Objetos destruídos pelo Destroy também são considerados null pela Unity
            if (enemy != null)
            {
                return false;
            }
        }

        return true;
    }
}