#region 2. Добавить новый тип снаряда.
using UnityEngine;
namespace Geekbrains
{
	public sealed class MyBullet : Ammunition
	{
        bool doubleDamage;
        public bool DoubleDamage { set => doubleDamage = value; get => doubleDamage; }

		private void OnCollisionEnter(UnityEngine.Collision collision)
		{
			var tempObj = collision.gameObject.GetComponent<ISetDamage>();
            // дописать доп урон
            #region 5. Добавить изменения текущего урона, который может нанести пуля.
            if (!doubleDamage)
                tempObj?.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
            else
                tempObj?.SetDamage(new InfoCollision(_curDamage * 2, Rigidbody.velocity));
            #endregion

            //var tempObj1 = collision.gameObject.GetComponent<IDefence>();

            //tempObj1?.ChangeSetDamage()


            //if (tempObj)

            //if (collision.gameObject.GetComponent<ISetDamage>())
            //if (tempObj.)

            //if (collision.gameObject.GetComponent<IDefence>())
            //    tempObj?.SetDamage(new InfoCollision(_curDamage / 2, Rigidbody.velocity));




            if (GetComponentInChildren<ParticleSystem>())
            {
               // Debug.Log("SADSAD");
                GetComponentInChildren<ParticleSystem>().gameObject.AddComponent<SelfDestroy>();
                GetComponentInChildren<ParticleSystem>().transform.GetComponent<SelfDestroy>().SelfDestroyTime(1f);
                //GetComponentInChildren<ParticleSystem>().transform.localScale = this.transform.localScale; // должно решить проблему изменения размера после смены родителя
                GetComponentInChildren<ParticleSystem>().transform.parent = null;
            }

            Destroy(gameObject);
		}
    }
}
#endregion