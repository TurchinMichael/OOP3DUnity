using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{

    public class StepAudio : MonoBehaviour
    {
        public List<AudioClip> audioList;
        public float distanceToFloor;
        public Transform stepRayStart;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StepChangeAudio()
        {
            RaycastHit hit;

           // Debug.Log(_audioSource.clip);

            Debug.DrawRay(stepRayStart.position, new Vector3(stepRayStart.position.x, stepRayStart.position.y - distanceToFloor, stepRayStart.position.z), Color.green);

            if (Physics.Linecast(stepRayStart.position, new Vector3(stepRayStart.position.x, stepRayStart.position.y - distanceToFloor), out hit))
            {

                Debug.Log(hit.transform.tag);

                if (hit.transform.tag == "Hollow")
                    _audioSource.clip = audioList[0];

                if (hit.transform.tag == "Terrain")
                    _audioSource.clip = audioList[1];

                if (hit.transform.tag == "Wood")
                    _audioSource.clip = audioList[2];
            }

            _audioSource.Play();
        }
    }
}