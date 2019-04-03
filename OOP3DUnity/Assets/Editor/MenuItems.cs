using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using GeekBrains.Data;

namespace Geekbrains.Editor
{
    public class MenuItems : EditorWindow
    {
        List<int> _createdGameObjects;
        GameObject _aid, _bomb;
        int _countAid, _countBomb = 1;




        private static StreamData _streamDataAid, _streamDataBomb; // : IData

        [MenuItem("Geekbrains/Cоздание бомб и аптечек &c", false, 0)]
        private static void NewMenuOption() // метод вызывающися из меню Geekbrains/Cоздание бомб и аптечек &c + горячие клавиши Alt + C
        {

            var path = Application.dataPath;

            _streamDataAid = new StreamData();
            _streamDataBomb = new StreamData();

            _streamDataAid?.SetOptions("Aid", path);
            _streamDataBomb?.SetOptions("Bomb", path);


           // Debug.Log(_streamDataAid.ToString());

            GetWindow(typeof(MenuItems));
        }




        

        private void OnGUI()
        {
            GUILayout.Label("Создание бомб и аптечек", EditorStyles.boldLabel);

            _countAid = EditorGUILayout.IntSlider("Количество аптечек", _countAid, 1, 10);
            _aid =
               EditorGUILayout.ObjectField("Aid Kit", _aid, typeof(GameObject), true)
                   as GameObject;

            _countBomb = EditorGUILayout.IntSlider("Количество бомб", _countBomb, 1, 10);
            _bomb =
               EditorGUILayout.ObjectField("Bomb", _bomb, typeof(GameObject), true)
                   as GameObject;
            



            if (GUILayout.Button("Создать объекты"))
            {
                if (_aid != null)
                    for (int i = 0; i < _countAid; i++)
                        _streamDataAid.Save((int)Instantiate(_aid, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());

                //_createdGameObjects.Add((int)Instantiate(_aid, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());

                if (_bomb != null)
                    for (int i = 0; i < _countBomb; i++)
                        _streamDataBomb.Save((int)Instantiate(_bomb, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());

                //_createdGameObjects.Add((int)Instantiate(_bomb, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());
            }




            if (GUILayout.Button("Стереть созданные объекты"))
            {
                List<int> _tsd = new List<int>(); // temp stream data
                _tsd = _streamDataAid.LoadId();

                if (_tsd.Count > 0)
                        for (int i = 0; i < _tsd.Count; i++)
                            DestroyImmediate(EditorUtility.InstanceIDToObject(_tsd[i]));

                _tsd = _streamDataBomb.LoadId();

                if (_tsd.Count > 0)
                    for (int i = 0; i < _tsd.Count; i++)
                        DestroyImmediate(EditorUtility.InstanceIDToObject(_tsd[i]));

            }

            //if (GUILayout.Button("Стереть созданные объекты"))
            //    if (_createdGameObjects.Count > 0)
            //        for (int i = 0; i < _createdGameObjects.Count; i++)
            //        {
            //            DestroyImmediate(EditorUtility.InstanceIDToObject(_createdGameObjects[i]));
            //        }
        }
    }
}