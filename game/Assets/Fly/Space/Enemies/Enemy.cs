namespace Fly.Space.Enemies
{
    public class Enemy : UnityEngine.MonoBehaviour
    {
        public float Power = 50f;
        public int Health = 500;
        public int Damage = 100;
        public UnityEngine.GameObject[] garbageObjects;

        public void Hit(int Value)
        {
            Value = UnityEngine.Mathf.Clamp(Value, 0, this.Health);

            this.Health -= Value;

            if (this.Health <= 0)
            {
                this.Explode();

                UnityEngine.GameObject.Destroy(this.gameObject);
            }
        }

        void OnCollisionEnter2D(UnityEngine.Collision2D Collision)
        {
            UnityEngine.GameObject GameObject = Collision.gameObject;

            while (GameObject.transform.parent != null)
            {
                GameObject = GameObject.transform.parent.gameObject;
            }

            //

            Fly.Ship.Instances.Ship Ship = GameObject.GetComponentInParent<Fly.Ship.Instances.Ship>();
            if (Ship)
            {
                Ship.Hit(Damage);

                UnityEngine.GameObject.Destroy(this.gameObject);
            }
        }

        private void Explode()
        {
            foreach (UnityEngine.GameObject garbObjType in garbageObjects)
            {
                UnityEngine.GameObject GameObject = Instantiate(garbObjType, this.transform.position, UnityEngine.Quaternion.identity);
                UnityEngine.Vector2 Velocity = UnityEngine.Random.insideUnitCircle * this.Power;
                UnityEngine.Rigidbody2D RigitBody = GameObject.GetComponent<UnityEngine.Rigidbody2D>();
                if (RigitBody)
                {
                    continue;
                }

                RigitBody.velocity = Velocity;
            }
        }
    }
}