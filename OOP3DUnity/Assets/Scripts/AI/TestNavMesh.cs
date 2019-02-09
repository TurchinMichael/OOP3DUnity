
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

            //GameObject.fi
            //setTargets = GameObject.FindObjectsOfType(System.Type.GetType(nameof(ISetTarget)));





            //setTargets = Object.FindSceneObjectsOfType<ISetTarget>();
            //setTargets = Object.FindObjectOfType<Geekbrains.ISetTarget>() as Object[];

            //setTargets = FindObjectOfType<ISetTarget>(); //GameObject.FindObjectsOfType<ISetTarget>();// (typeof(ISetTarget));

            targetS = new Transform[maxPointsCount];

            Zeroing();

            // NavMeshPath[] temp 
            _path = new NavMeshPath[targetS.Length - 1];

            for (int i = 0; i < _path.Length; i++)
                _path[i] = new NavMeshPath();

            //Debug.Log(_path.Length);
            //_path = temp;
            _elapsed = 0;
            if (GetComponent<LineRenderer>())
                lineRenderer = GetComponent<LineRenderer>();

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

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = new Vector3(hit.point.x, hit.point.y + sphere.transform.localScale.y / 0.85f, hit.point.z);
                    sphere.AddComponent<Destination>();

                    //setTarget.GetComponent<ISetTarget>().SetTarget(sphere.transform);


                    if (counter >= maxPointsCount)
                    {
                        Transform temp = targetS[0];

                        for (int j = 0; j < maxPointsCount - 1; j++)
                        {

                            targetS[j] = targetS[j + 1];
                        }
                        counter = maxPointsCount - 1;
                       // Debug.Log("temp");
                    }

                    targetS[counter] = sphere.transform;
                    // new Vector3(hit.point.x, hit.point.y, hit.point.z);

                    //#region stack
                    //if (sv.Count < maxPointsCount)
                    //    sv.Push(hit.point);
                    //#endregion
                    if (targetS[0] != null)
                        setTarget.GetComponent<ISetTarget>().SetTarget(targetS[0]);
                }
                

                _path = new NavMeshPath[targetS.Length - 1];

                for (int i = 0; i < _path.Length; i++)
                    _path[i] = new NavMeshPath();




                //// lineRenderer.numPositions = _path.Length;

                ////List<Vector3> temp = new List<Vector3>();



                #region vector3CalcDraw
                CalculatePathVector3();
                DrawLinesVector3();
                #endregion


                counter++;
                //Debug.Log(counter);
            }


            //if (Input.GetKeyDown(KeyCode.Alpha1)) // болванчик уткнулся в первую точку
            //{
            //    HasCome();
            //}


            //_elapsed += Time.deltaTime;
            //if (_elapsed > 1)
            //{
            //    _elapsed = -1;

            //    _path = new NavMeshPath[targetS.Length - 1];
            //    for (int i = 0; i < _path.Length; i++)
            //        _path[i] = new NavMeshPath();


            //    for (var i = 0; i < targetS.Length - 1; i++)
            //    {
            //        NavMesh.CalculatePath(targetS[i]/*.position*/, targetS[i + 1]/*.position*/, NavMesh.AllAreas, _path[i]);
            //    }
            //}

            //// lineRenderer.numPositions = _path.Length;

            ////List<Vector3> temp = new List<Vector3>();


            //lineRenderer.positionCount = 0;

            //for (var i = 0; i < _path.Length; i++)
            //{
            //    int tempI = lineRenderer.positionCount;
            //    lineRenderer.positionCount = lineRenderer.positionCount + _path[i].corners.Length;

            //    Debug.Log(_path[i].corners.Length + "   " + _path.Length + "  " + tempI);

            //    for (var j = 0; j < _path[i].corners.Length; j++)
            //    {
            //        //Debug.DrawLine(_path[i].corners[j], _path[i].corners[j + 1], Color.red);

            //        //temp.Add(_path[i].corners[j]);

            //        lineRenderer.SetPosition(tempI + j, _path[i].corners[j]);
            //        //Debug.Log(lineRenderer.positionCount);
            //    }
            //}

            ////lineRenderer.positionCount = temp.Count; // _path[i].corners.Length;

            ////for (int i = 0; i < temp.Count; i++)
            ////{
            ////    lineRenderer.SetPosition(i,temp[i]);
            ////}

        }


        public void HasCome()
        {

           // Debug.Log("VSE");

            if (counter > maxPointsCount)
                counter = maxPointsCount;


            // Debug.Log(counter);
            #region vector3CalcDraw
            //targetS[0] = Vector3.zero;
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
            CalculatePathVector3();
            DrawLinesVector3();
            #endregion


            setTarget.GetComponent<ISetTarget>().SetTarget(targetS[0]);
        }

        //#region Stack
        //void CalculatePathStack()
        //{


        //    //foreach (var obj in sv)
        //    //{

        //    //}


        //    //for (var i = 0; i < sv.Count - 1; i++)
        //    //{
        //    //    //if (targetS[i + 1] != Vector3.zero && targetS[i] != Vector3.zero)
        //    //    //    NavMesh.CalculatePath(targetS[i]/*.position*/, targetS[i + 1]/*.position*/, NavMesh.AllAreas, _path[i]);                                

        //    //}
        //}

        //#endregion

        #region Vector3
        void Zeroing()
        {
            for (int i = 0; i < targetS.Length; i++)// obj in targetS)
                targetS[i] = null;// Vector3.zero;
        }

        bool check;

        void CalculatePathVector3()
        {
            for (var i = 0; i < targetS.Length - 1; i++)
            {
                if (targetS[i] != null/*Vector3.zero*/ & targetS[i + 1] != null/*Vector3.zero*/)
                    NavMesh.CalculatePath(targetS[i].position, targetS[i + 1].position, NavMesh.AllAreas, _path[i]);
            }
        }


        void DrawLinesVector3()
        {

            lineRenderer.positionCount = 0;

            for (var i = 0; i < _path.Length; i++)
            {
                int tempI = lineRenderer.positionCount;
                lineRenderer.positionCount = lineRenderer.positionCount + _path[i].corners.Length;

                //Debug.Log(_path[i].corners.Length + "   " + _path.Length + "  " + tempI);

                for (var j = 0; j < _path[i].corners.Length; j++)
                {
                    lineRenderer.SetPosition(tempI + j, _path[i].corners[j]);
                }
            }
        }

        #endregion
    }
}
