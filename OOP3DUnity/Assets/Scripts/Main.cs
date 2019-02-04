using System.Collections;
using UnityEngine;

namespace Geekbrains
{
	public class Main : MonoBehaviour
	{
		public FlashLightController FlashLightController { get; private set; }
		public InputController InputController { get; private set; }
		public PlayerController PlayerController { get; private set; }
        public OutlinedController OutlinedController { get; private set; }
        public WeaponController WeaponController { get; private set; }


        //public DragRigidbodyController DragRigidbodyController { get; private set; }


        public ObjectManager ObjectManager { get; private set; }
        public Transform Player { get; private set; }
        public Transform MainCamera { get; private set; }
        private BaseController[] Controllers;

		public static Main Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            ObjectManager = new ObjectManager();
            ObjectManager.Start();
            PlayerController = new PlayerController(new UnitMotor(
				GameObject.FindObjectOfType<CharacterController>().transform));
			PlayerController.On();
			FlashLightController = new FlashLightController();
			InputController = new InputController();
			InputController.On();
            OutlinedController = new OutlinedController(); // новый
            WeaponController = new WeaponController();


           // DragRigidbodyController = new DragRigidbodyController();

            Controllers = new BaseController[5];
			Controllers[0] = FlashLightController;
			Controllers[1] = InputController;
			Controllers[2] = PlayerController;
            Controllers[3] = OutlinedController;
            Controllers[4] = WeaponController;


            //Controllers[5] = DragRigidbodyController;
        }

		private void Update()
		{
			for (var index = 0; index < Controllers.Length; index++)
			{
				var controller = Controllers[index];
				controller.OnUpdate();
			}
        }
    }
}