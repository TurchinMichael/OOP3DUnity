using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseLight : MonoBehaviour
{
    List<GameObject> _objectsList = new List<GameObject>();

    private void Start()
    {
        if (_objectsList.Count < 1)
            foreach (Renderer mats in transform.GetComponentsInChildren<Renderer>())
                if (mats.material.shader.name == "Outlined/Uniform")
                    _objectsList.Add(mats.gameObject);
    }

    private void OnMouseOver()
    {
        foreach (GameObject _object in _objectsList)
        {
                _object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.03f);
        }
    }

    private void OnMouseExit()
    {
        foreach (GameObject _object in _objectsList)
        {
                _object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0f);
        }
    }
}
