using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepAudio : MonoBehaviour
{
    public List<AudioClip> audioList;
    public float distanceToFloor;
    public Transform stepRayStart;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;

        //Debug.DrawRay(stepRayStart.position, new Vector3(stepRayStart.position.x, stepRayStart.position.y - 1000f, stepRayStart.position.z), Color.green);

        if (Physics.Raycast(stepRayStart.position, new Vector3(stepRayStart.position.x, stepRayStart.position.y - distanceToFloor), out hit))
        {
            Debug.DrawRay(stepRayStart.position, new Vector3(stepRayStart.position.x, stepRayStart.position.y - 1000f, stepRayStart.position.z), Color.green);

            if (hit.transform.tag == "Hollow")
                _audioSource.clip = audioList[0];

            if (hit.transform.tag == "Terrain")
                _audioSource.clip = audioList[1];

            if (hit.transform.tag == "Wood")
                _audioSource.clip = audioList[2];

            Debug.Log(_audioSource.clip);
        }
    }
}
