using UnityEngine;

namespace Geekbrains
{
    public class AidKit : BaseObjectScene, IAid, IDestroyable
    {
        public float heal;
        InfoCollision _infoCollision;
        private void OnCollisionEnter(Collision collision)
        {

            if (collision.transform.tag == "Player" || collision.transform.tag == "Enemy")
            {
                _infoCollision = new InfoCollision(-heal, collision.contacts[0], collision.transform, Rigidbody.velocity);
                //Invoke(nameof(Heal), 1);// Explosion();

                Heal(collision);

                Debug.Log("Heal");
            }
        }


        public void Heal(Collision collision)
        {
            Bot _bot = collision.transform.GetComponent<Bot>();

            if (_bot != null)
                _bot.SetDamage(_infoCollision);
        }

        public void Destroy()
        {
            Destroy(gameObject, 1f);
        }
    }
}
