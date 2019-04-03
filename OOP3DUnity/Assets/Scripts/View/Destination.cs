using System.Collections;
using System.Collections.Generic;
using Tests;
using UnityEngine;

namespace Geekbrains
{
    public class Destination : MonoBehaviour, IDestination, IDestroyable
    {
        TestNavMesh testNavMesh;

        public void DestroySelfAfterCome()
        {
            testNavMesh.HasCome();
            //Debug.Log(name);
            //Destroy(gameObject);
            Destroy();
        }

        public void Destroy()
        {
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
