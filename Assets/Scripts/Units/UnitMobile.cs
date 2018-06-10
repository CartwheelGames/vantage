using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units
{
    [DisallowMultipleComponent]
    public class UnitMobile : UnitBase
    {
        [SerializeField]
        private float speed = 1f;
        protected void Update()
        {
            if (!IsDead)
            {
                transform.Translate(Time.deltaTime * speed * Vector3.forward);
            }
            else
            {
                transform.Translate(Vector3.down);
            }
        }
    }
}
