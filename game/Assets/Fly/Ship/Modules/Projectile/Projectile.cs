namespace Fly.Ship.Modules.Projectile
{
    public class Projectile : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private float _Speed = 100f;

        [UnityEngine.SerializeField]
        private int _Damage = 100;

        private void Start()
        {
            UnityEngine.Rigidbody2D RigidBody = this.GetComponent<UnityEngine.Rigidbody2D>();
            if (!RigidBody)
            {
                return;
            }

            RigidBody.velocity = this.transform.right * this._Speed;
        }

        private void OnBecameInvisible()
        {
            UnityEngine.GameObject.Destroy(this.gameObject);
        }

        public int GetDamage()
        {
            return _Damage;
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D Collider)
        {
            UnityEngine.GameObject GameObject = Collider.gameObject;

            while (GameObject.transform.parent != null)
            {
                GameObject = GameObject.transform.parent.gameObject;
            }

            //

            Fly.Space.Enemies.Enemy Enemy = GameObject.GetComponentInParent<Fly.Space.Enemies.Enemy>();
            if (Enemy)
            {
                Enemy.Hit(this._Damage);

                UnityEngine.GameObject.Destroy(this.gameObject);
            }
        }
    }
}
