
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Geekbrains;

namespace Tests
{
    public class TestNavMesh : MonoBehaviour
    {
        public
        Transform[] targetS;
        public int maxPointsCount = 3;

        private NavMeshPath[] _path;
        private float _elapsed = 0;
        LineRenderer lineRenderer;
        int mouseNumber = 2;
        Camera mainCamera;

        int counter;


        public GameObject/*List<*//*ISetTarget*//*>*/ setTarget;
        

        private void Start()
        {
            targetS = new Transform[maxPointsCount];

            Zeroing();

            // NavMeshPath[] temp 
            _path = new NavMeshPath[targetS.Length - 1];

            for (int i = 0; i < _path.Length; i++)
                _path[i] = new NavMeshPath();

            //Debug.Log(_path.Length);
            //_path = temp;
            _elapsed = 0;

            lineRenderer = GetComponent<LineRenderer>();
            if (!lineRenderer)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }

            mainCamera = Camera.main;
        }



        private void Update()
        {
            if (Input.GetMouseButtonDown(mouseNumber))//Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                                     mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100/*,
                                     customPhysicLayer*/))
                {
                //    if (hit.transform.tag == "Player")
                //        return;

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = new Vector3(hit.point.x, hit.point.y + sphere.transform.localScale.y / 0.85f, hit.point.z);
                    sphere.AddComponent<Destination>();                   // sphere.AddComponent<Destination>();
                    sphere.GetComponent<SphereCollider>().enabled = false;

                    if (counter >= maxPointsCount)
                    {
                        Transform temp = targetS[0];


                        targetS[0].GetComponent<Destination>().Destroy(); // удаление не входящей в максимум точки

                        for (int j = 0; j < maxPointsCount - 1; j++)
                        {
                            targetS[j] = targetS[j + 1];
                        }
                        counter = maxPointsCount - 1;
                        // Debug.Log("temp");
                    }

                    targetS[counter] = sphere.transform;

                    if (targetS[0] != null)
                        setTarget.GetComponent<ISetTarget>().SetTarget(targetS[0]);
                }


                _path = new NavMeshPath[targetS.Length - 1];

                for (int i = 0; i < _path.Length; i++)
                    _path[i] = new NavMeshPath();



                #region vector3CalcDraw
                CalculatePathVector();
                DrawLinesVector();
                #endregion


                counter++;
            }

        }


        public void HasCome()
        {

            // Debug.Log("VSE");

            if (counter > maxPointsCount)
                counter = maxPointsCount;


            // Debug.Log(counter);

            #region vector3CalcDraw
            
            if (counter > 1)
            {
                for (int j = 0; j < maxPointsCount - 1; j++)
                {
                    targetS[j] = targetS[j + 1];
                }
                counter--;
            }
            else
            {
                Zeroing();
                counter = 0;
            }
            #endregion

            #region vector3CalcDraw
            CalculatePathVector();
            DrawLinesVector();
            #endregion


            setTarget.GetComponent<ISetTarget>().SetTarget(targetS[0]);


            if (CheckClearTargets())
                ClearLines(); // чистка
        }

        #region Vector3
        void Zeroing()
        {
            for (int i = 0; i < targetS.Length; i++)// obj in targetS)
                targetS[i] = null;// Vector3.zero;
        }

        bool check;

        void CalculatePathVector()
        {
            for (var i = 0; i < targetS.Length - 1; i++)
            {
                if (targetS[i] != null/*Vector3.zero*/ & targetS[i + 1] != null/*Vector3.zero*/)
                {
                    //Debug.Log(0);
                    NavMesh.CalculatePath(targetS[i].position, targetS[i + 1].position, NavMesh.AllAreas, _path[i]);
                }
                //else
                //{
                //    ClearLines();
                //    if (CheckClearTargets())
                //    {
                //        ClearLines();
                //    }
                //}
                //        lineRenderer.positionCount = 0;
                //        DrawLinesVector();
                //        Debug.Log(1);
                //    }
                //}
            }
        }


        void DrawLinesVector()
        {

            lineRenderer.positionCount = 0;

            for (var i = 0; i < _path.Length; i++)
            {
                int tempI = lineRenderer.positionCount;
                lineRenderer.positionCount = lineRenderer.positionCount + _path[i].corners.Length;

                for (var j = 0; j < _path[i].corners.Length; j++)
                    lineRenderer.SetPosition(tempI + j, _path[i].corners[j]);
            }

            //if (CheckClearTargets())
            //    lineRenderer.positionCount = 0;
        }

        void ClearLines()
        {
            lineRenderer.positionCount = 0;
        }

        bool CheckClearTargets()
        {
            for (int i = 0; i < targetS.Length; i++)
                if (targetS[i] != null)
                    return false;

            return true;
        }
        #endregion
    }
}
