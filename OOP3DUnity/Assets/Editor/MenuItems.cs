using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Geekbrains.Editor
{
    #region 1. Разработать свое окно редактора, добавить в него базовые составляющие интерфейса.
    public class MenuItems : EditorWindow
    #endregion
    {
        List<int> _createdGameObjects;
        GameObject _aid, _bomb;
        int _countAid, _countBomb = 1;

        #region 2. Реализовать вызов разработанного окна с помощью меню.
        [MenuItem("Geekbrains/Cоздание бомб и аптечек &c", false, 0)]
        private static void NewMenuOption() // метод вызывающися из меню Geekbrains/Cоздание бомб и аптечек &c + горячие клавиши Alt + C
        {
            GetWindow(typeof(MenuItems));
        }
        #endregion

        private void OnGUI()
        {
            GUILayout.Label("Создание бомб и аптечек", EditorStyles.boldLabel);

            #region 3. Расширить функционал скрипта в окне инспектора. / 6. Добавить еще несколько типов объектов для сохранения.
            _countAid = EditorGUILayout.IntSlider("Количество аптечек", _countAid, 1, 10);
            _aid =
               EditorGUILayout.ObjectField("Aid Kit", _aid, typeof(GameObject), true)
                   as GameObject;

            _countBomb = EditorGUILayout.IntSlider("Количество бомб", _countBomb, 1, 10);
            _bomb =
               EditorGUILayout.ObjectField("Bomb", _bomb, typeof(GameObject), true)
                   as GameObject;
            #endregion


            #region 7. *Разработать свой алгоритм расстановки объектов.
            if (GUILayout.Button("Создать объекты"))
            {
                if (_aid != null)
                    for (int i = 0; i < _countAid; i++)
                        _createdGameObjects.Add((int)Instantiate(_aid, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());

                if (_bomb != null)
                    for (int i = 0; i < _countBomb; i++)
                        _createdGameObjects.Add((int)Instantiate(_bomb, Patrol.GenericPoint(2f), Quaternion.identity).GetInstanceID());
            }
            #endregion

            #region 5. Придумать и реализовать алгоритм очистки сцены от объектов.
            if (GUILayout.Button("Стереть созданные объекты"))
                if (_createdGameObjects.Count > 0)
                    for (int i = 0; i < _createdGameObjects.Count; i++)
                        DestroyImmediate(EditorUtility.InstanceIDToObject(_createdGameObjects[i]));
            #endregion
        }
    }
}