using UnityEngine;

namespace Geekbrains
{
	public class FlashLightController : BaseController
	{
		private FlashLightModel _flashLight;
		private FlashLightUiText _flashLightUi;
        private FlashLightUiImageIndicator _flashLightUiImageIndicator;

        public FlashLightController()
		{
			_flashLight = MonoBehaviour.FindObjectOfType<FlashLightModel>();
			_flashLightUi = MonoBehaviour.FindObjectOfType<FlashLightUiText>();
            _flashLightUiImageIndicator = MonoBehaviour.FindObjectOfType<FlashLightUiImageIndicator>();
            Off();
		}

		public override void OnUpdate()
        {
            _flashLightUiImageIndicator.ImageIndicator = _flashLight.BatteryChargeCurrent / _flashLight.BatteryChargeMax;   // b.Вывод на экран заряда батареи

            if (!IsActive)
            {
                _flashLight.Charging();
                return;
            }

			if (_flashLight == null)return;
			_flashLight.Rotation();

			if (_flashLight.EditBatteryCharge())
			{
				_flashLightUi.Text = _flashLight.BatteryChargeCurrent;
            }
			else
			{
				Off();
			}            
		}

		public override void On()
		{
			if (IsActive)return;
			base.On();
			_flashLight.Switch(true);
			_flashLightUi.SetActive(true);
		}

		public sealed override void Off()
		{
			if (!IsActive) return;
			base.Off();
			_flashLight.Switch(false);
			_flashLightUi.SetActive(false);
		}
	}
}