using System;
using UnityEngine;

namespace Geekbrains
{
	public class Aim : MonoBehaviour, ISetDamage
	{
		public event Action OnPointChange;

		public float Hp = 100;
        public bool isPotected;

		private bool _isDead;
		// дописать поглащение урона
		public void SetDamage(InfoCollision info)
		{
			if (_isDead) return;
			if (Hp > 0)
            {
                #region 5. Добавить изменения текущего урона, который может нанести пуля. / 4. Добавить двух противников с разной обработкой получения урона.
                if (!isPotected)
                    Hp -= info.Damage * 2;
                else
                    Hp -= info.Damage;
                #endregion
            }

            if (Hp <= 0)
			{
				var tempRigidbody = GetComponent<Rigidbody>();
				if (!tempRigidbody)
				{
					tempRigidbody = gameObject.AddComponent<Rigidbody>();
				}
				tempRigidbody.velocity = info.Dir;
				Destroy(gameObject, 10);

				OnPointChange?.Invoke();
				_isDead = true;
			}
		}
	}
}