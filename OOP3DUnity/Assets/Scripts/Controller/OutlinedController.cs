using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class OutlinedController : BaseController // Добавить свой контроллер (например, для выделения объектов)
    {
        RaycastHit _hit;
        Ray _ray;
        Camera _camera;

        public OutlinedController()
        {
            _camera = Camera.main;

            #region Так себе вариант, и шедейра нормального не нашел
            foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.GetComponent<Renderer>() && obj.GetComponent<Renderer>().material.shader.name == "Outlined/Uniform")
                    obj.AddComponent<onMouseLight>();
            }
            #endregion
        }

        public override void OnUpdate()
        {
            #region Добавить функции для изменения свойств объектов (например, изменения слоя объекта, заморозка физического объекта по определенной оси, включение/выключение физики объекта)
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
                if (_hit.collider != null)
                {
                    if (_hit.collider.gameObject.GetComponent<Renderer>() && _hit.collider.gameObject.tag == "colorChange")
                        _hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;

                    if (_hit.collider.gameObject.GetComponent<Rigidbody>() && _hit.collider.gameObject.tag == "physicChange")
                        _hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                    if (_hit.collider.gameObject.GetComponent<Rigidbody>() && _hit.collider.gameObject.tag == "wallOff")
                        _hit.collider.gameObject.layer = LayerMask.NameToLayer("AntiPhysics");
                }
            #endregion
        }
    }
}