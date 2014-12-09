using UnityEngine;

    public abstract class Controller : MonoBehaviour
    {
        public int Health
        {
            get
            {
                return healthBar.Health;
            }
            set
            {
                healthBar.Health = value;
            }
        }
        public HealthBarRenderer healthBar;
    }
