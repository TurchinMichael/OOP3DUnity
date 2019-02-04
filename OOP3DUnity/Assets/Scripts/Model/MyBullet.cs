using UnityEngine;

namespace Geekbrains
{
	public sealed class MyBullet : Ammunition
	{
		private void OnCollisionEnter(UnityEngine.Collision collision)
		{
			var tempObj = collision.gameObject.GetComponent<ISetDamage>();
			// дописать доп урон
			tempObj?.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));

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