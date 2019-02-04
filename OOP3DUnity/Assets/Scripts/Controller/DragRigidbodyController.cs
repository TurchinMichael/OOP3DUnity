using System.Collections;
using UnityEngine;

namespace Geekbrains
{
    public class DragRigidbodyController : MonoBehaviour
    {
        public LayerMask customPhysicLayer;
        [SerializeField] float k_Spring = 50.0f; // >> private and next
        [SerializeField] float k_Damper = 5.0f;
        [SerializeField] float k_Drag = 10.0f;
        [SerializeField] float k_AngularDrag = 5.0f;
        [SerializeField] float k_Distance = 0.2f;
        [SerializeField] float distanceToObject = 4f;
        [SerializeField] float throwForce = 1000f;
        [SerializeField] float distance = 2f;

        const bool k_AttachToCenterOfMass = false;

        private SpringJoint m_SpringJoint;

        //Input _input = Input.GetMouseButton(0);

        bool _check;

        // bool _key = Input.GetMouseButton(0);// KeyCode.Mouse0;

        int mouseNumber;


        public DragRigidbodyController()
        {
            mouseNumber = 1;
            _check = false;

            customPhysicLayer = -1;
        }



        //  public override void OnUpdate()  public void Update()
        public void Update()
        {

            // Debug.Log(customPhysicLayer.value);

            // Make sure the user pressed the mouse down
            if (!Input.GetMouseButtonDown(mouseNumber)|| _check)//Input.GetMouseButtonDown(0))
            {
                return;
            }

            var mainCamera = FindCamera();

            // Debug.Log(mainCamera.transform.name);
            // We need to actually hit an object
            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                                 mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
                                 customPhysicLayer)) // Physics.DefaultRaycastLayers)
            {
                //Debug.Log(hit.transform.name);
                return;
            }
            //Debug.Log(hit.transform.name);
            //Debug.Log(mainCamera.transform.name);
            if (hit.distance > distanceToObject)
            {
                return;
            }
            // We need to hit a rigidbody that is not kinematic
            if (!hit.rigidbody || hit.rigidbody.isKinematic)
            {
                return;
            }


            if (!m_SpringJoint)
            {
                var go = new GameObject("Rigidbody dragger");
                Rigidbody body = go.AddComponent<Rigidbody>();
                m_SpringJoint = go.AddComponent<SpringJoint>();
                body.isKinematic = true;
            }
            m_SpringJoint.transform.position = hit.point;
            m_SpringJoint.anchor = Vector3.zero;

            m_SpringJoint.spring = k_Spring;
            m_SpringJoint.damper = k_Damper;
            m_SpringJoint.maxDistance = k_Distance;
            m_SpringJoint.connectedBody = hit.rigidbody;
            m_SpringJoint.connectedMassScale = 30;


            //Debug.Log(hit.transform.name);
            _check = true;


            //MainCoroutine(0);//"DragObject", hit.distance);

            //base.MainCoroutine(0);//запуск первой доступной корутины
            //DoStartCoroutine(/*nameof(*/DragObject()/*), hit.distance*/);
            //DoStartCoroutine(nameof(DragObject));
            StartCoroutine(nameof(DragObject));
            //    MonoBehaviour monoBehaviour = new MonoBehaviour();
            //monoBehaviour.StartCoroutine(nameof(DragObject), hit.distance);
            //MonoBehaviour.Instantiate.StartCoroutine(nameof(DragObject), hit.distance);
        }



        private IEnumerator DragObject(/*float distance*/)
        {
            var oldDrag = m_SpringJoint.connectedBody.drag;
            //Debug.Log(oldDrag);
            var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
            m_SpringJoint.connectedBody.drag = k_Drag;
            m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
            var mainCamera = FindCamera();

            while (Input.GetMouseButton(mouseNumber))
            {
                //Debug.Log(_key);

                if (Input.GetKeyDown(KeyCode.E) && m_SpringJoint.connectedBody) //(Input.GetButtonDown("Use"))
                {
                    //  Debug.Log("SDFSDF");
                    throwObject(m_SpringJoint.connectedBody, oldDrag, oldAngularDrag);
                    _check = false;
                    yield return null;
                }

                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                m_SpringJoint.transform.position = ray.GetPoint(distance);
                yield return null;
            }

            if (m_SpringJoint.connectedBody)
            {
                //Debug.Log("connectedBody");
                m_SpringJoint.connectedBody.drag = oldDrag;
                m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
                m_SpringJoint.connectedBody = null;
                _check = false;
            }
        }



        void throwObject(Rigidbody rdb, float oldDrag, float oldAngularDrag)
        {
            Rigidbody tempRigidbody = rdb;

            rdb.drag = oldDrag;
            rdb.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;

            tempRigidbody.AddForce(FindCamera().transform.forward * throwForce);
        }

        private Camera FindCamera()
        {
            //if (GetComponent<Camera>())
            //{
            //    return GetComponent<Camera>();
            //}

            return Camera.main;
        }
    }
}