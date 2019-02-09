using System.Collections;
using System.Collections.Generic;
using Tests;
using UnityEngine;

namespace Geekbrains
{
    public class Destination : MonoBehaviour, IDestination
    {
        TestNavMesh testNavMesh;

        public void DestroySelf()
        {
            testNavMesh.HasCome();
            //Debug.Log(name);
            Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            testNavMesh = FindObjectOfType<TestNavMesh>();
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}
