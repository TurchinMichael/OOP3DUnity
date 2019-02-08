
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tests
{
    public class TestNavMesh : MonoBehaviour
    {
        public Vector3[] targetS;
        public int maxPointsCount = 3;

        private NavMeshPath[] _path;
        private float _elapsed = 0;
        LineRenderer lineRenderer;
        int mouseNumber = 2;
        Camera mainCamera;


        private void Start()
        {
            // NavMeshPath[] temp 
            _path = new NavMeshPath[targetS.Length - 1];

            for (int i = 0; i < _path.Length; i++)
                _path[i] = new NavMeshPath();

            //Debug.Log(_path.Length);
            //_path = temp;
            _elapsed = 0;
            if (GetComponent<LineRenderer>())
                lineRenderer = GetComponent<LineRenderer>();

            mainCamera = Camera.main; ;
            targetS = new Vector3[maxPointsCount];
            for (int i =0; i < targetS.Length; i++)// obj in targetS)
                targetS[i] = Vector3.zero;
        }

        private void Update()
        {



            if (!Input.GetMouseButtonDown(mouseNumber))//Input.GetMouseButtonDown(0))
            {

                // Debug.Log(mainCamera.transform.name);
                // We need to actually hit an object
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                                     mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100/*,
                                     customPhysicLayer*/)) // Physics.DefaultRaycastLayers)
                {
                    //Debug.Log(hit.transform.name);
                    //return;
                    hit.point;
                }

            }


            _elapsed += Time.deltaTime;
            if (_elapsed > 1)
            {
                _elapsed = -1;

                _path = new NavMeshPath[targetS.Length - 1];
                for (int i = 0; i < _path.Length; i++)
                    _path[i] = new NavMeshPath();


                for (var i = 0; i < targetS.Length - 1; i++)
                {
                    NavMesh.CalculatePath(targetS[i].position, targetS[i + 1].position, NavMesh.AllAreas, _path[i]);
                }
            }

            // lineRenderer.numPositions = _path.Length;

            //List<Vector3> temp = new List<Vector3>();


            lineRenderer.positionCount = 0;

            for (var i = 0; i < _path.Length; i++)
            {
                int tempI = lineRenderer.positionCount;
                lineRenderer.positionCount = lineRenderer.positionCount + _path[i].corners.Length;

                Debug.Log(_path[i].corners.Length + "   " + _path.Length + "  " + tempI);

                for (var j = 0; j < _path[i].corners.Length; j++)
                {
                    //Debug.DrawLine(_path[i].corners[j], _path[i].corners[j + 1], Color.red);

                    //temp.Add(_path[i].corners[j]);

                    lineRenderer.SetPosition(tempI + j, _path[i].corners[j]);
                    //Debug.Log(lineRenderer.positionCount);
                }
            }

            //lineRenderer.positionCount = temp.Count; // _path[i].corners.Length;

            //for (int i = 0; i < temp.Count; i++)
            //{
            //    lineRenderer.SetPosition(i,temp[i]);
            //}

        }
    }
}
