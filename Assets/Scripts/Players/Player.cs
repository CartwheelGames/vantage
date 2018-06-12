using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Teams;
namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject currentPlatform;
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
        private Camera myCamera;


        private void Start()
        {
            myCamera = gameObject.GetComponent<Camera>();
        }

        private void Update()
        {
            GameObject objectHitting = GetObjectHitting();

            bool isMouseDown = Input.GetMouseButtonDown(0);

            if (IsEnemy(objectHitting))
            {
                Debug.Log("MOB");
                UnitBase hitUnit = objectHitting.GetComponent<UnitBase>();
                hitUnit.TakeDamage(5);
            }
            else if (IsPlatform(objectHitting) && isMouseDown)
            {
                Debug.Log("PLATFORM");
                GameObject teleportPoint = objectHitting.GetComponent<Platform>().TeleportPoint;
                transform.position = teleportPoint.transform.position;
                transform.rotation = teleportPoint.transform.rotation;
            }

        }

        private GameObject GetObjectHitting()
        {
            RaycastHit hit;

            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {

                return hit.collider.gameObject;
            }

            return null;
        }


        private bool IsEnemy(GameObject target)
        {
            if (target != null)
            {
                UnitBase targetUnit = target.GetComponent<UnitBase>();
                return targetUnit != null && targetUnit.MyTeam != myTeam;
            }
            return false;
        }

        private bool IsPlatform(GameObject target)
        {
            return target != null && target.GetComponent<Platform>() != null;
        }
    }
}
