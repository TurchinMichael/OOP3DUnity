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


        public DragRigidbodyController DragRigidbodyController { get; private set; }


        public BotController BotController { get; private set; }
        public ObjectManager ObjectManager { get; private set; }
        public Transform Player { get; private set; }
        public Transform MainCamera { get; private set; }
        private BaseController[] Controllers;

		public static Main Instance { get; private set; }
        public Bot RefBotPrefab;
        public int CountBot;

        private void Awake()
		{
			Instance = this;

            Player = GameObject.FindGameObjectWithTag("Player").transform;
           // Player = GameObject.FindGameObjectWithTag("AIThirdPersonController").transform;
            

            ObjectManager = new ObjectManager();
            PlayerController = new PlayerController(new UnitMotor(
				GameObject.FindObjectOfType<CharacterController>().transform));
			PlayerController.On();
			FlashLightController = new FlashLightController();
			InputController = new InputController();
			InputController.On();
            OutlinedController = new OutlinedController();
            WeaponController = new WeaponController();

            BotController = new BotController();

            DragRigidbodyController = new DragRigidbodyController();


            Controllers = new BaseController[7];
			Controllers[0] = FlashLightController;
			Controllers[1] = InputController;
			Controllers[2] = PlayerController;
            Controllers[3] = OutlinedController;
            Controllers[4] = WeaponController;
            Controllers[5] = DragRigidbodyController;
            Controllers[6] = BotController;
        }

        private void Start()
        {
            ObjectManager.Start();
            InputController.On();
            PlayerController.On();
            BotController.On();
            BotController.Init(CountBot);
        }

        private void Update()
		{
			for (var index = 0; index < Controllers.Length; index++)
			{
				var controller = Controllers[index];
				controller.OnUpdate();
			}
        }


        public void DoStartCoroutine(IEnumerator routine)
        { StartCoroutine(routine); }

        //public void DoStartCoroutine(string routine)
        //{ StartCoroutine(routine); }
        //public void DoStartCoroutine(string routine, float parameter)
        //{ StartCoroutine(routine, parameter); }

        //static void DoStopCoroutine(IEnumerator routine)
        //{ StopCoroutine(routine); }

        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), $"{1 / Time.deltaTime:0.0}");
        }
    }
}