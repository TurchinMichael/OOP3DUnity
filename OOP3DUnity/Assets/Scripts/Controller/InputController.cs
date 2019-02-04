using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController
	{
		private KeyCode _codeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        
        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        int counter = 0;

        public override void OnUpdate()
		{
			if (!IsActive) return;

            #region 6. Добавить смену оружия по колесику мыши.
            float mw = Input.GetAxis("Mouse ScrollWheel");

            if (mw > 0)
                counter += 1;

            if (mw < 0)
                counter -= 1;
            
            if (counter == Main.Instance.ObjectManager.Weapons.Length)
                Main.Instance.WeaponController.Off();

            if (counter > Main.Instance.ObjectManager.Weapons.Length)
                counter = 0;

            if (counter < 0)
                counter = Main.Instance.ObjectManager.Weapons.Length;
            
             if (counter >= 0 && counter < Main.Instance.ObjectManager.Weapons.Length)
                SelectWeapon(counter);

            //Debug.Log(counter);

            #endregion

            if (Input.GetKeyDown(_codeFlashLight))
			{
				Main.Instance.FlashLightController.Switch();
            }

            // реализовать выбор оружия по колесику мыши

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }

            if (Input.GetKeyDown(_cancel))
            {
                Main.Instance.WeaponController.Off();
                Main.Instance.FlashLightController.Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                Main.Instance.WeaponController.ReloadClip();
            }
        }

        private void SelectWeapon(int i)
        {
            Main.Instance.WeaponController.Off();
            var tempWeapon = Main.Instance.ObjectManager.Weapons[i];
            if (tempWeapon != null) Main.Instance.WeaponController.On(tempWeapon);
        }
    }
}