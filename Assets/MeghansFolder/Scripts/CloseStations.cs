using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseStations : MonoBehaviour
{
    public GameObject stations;
    public GameObject station1;
    public GameObject station2;
    public GameObject station3;
    public GameObject station4;

    public void OpenStations()
    {
        stations.SetActive(true);
        station2.SetActive(false);
        station3.SetActive(false);
        station4.SetActive(false);
    }

    public void CloseStaions()
    {
        stations.SetActive(false);
        station2.SetActive(false);
        station3.SetActive(false);
        station4.SetActive(false);

    }
}
