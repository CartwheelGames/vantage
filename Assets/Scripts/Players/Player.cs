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
        private Platform currentPlatform = null;
        [SerializeField]
        private TeamData myTeam = null;
        public TeamData MyTeam { get { return myTeam; } }
        [SerializeField]
        private GameObject cursor = null;
        [SerializeField]
        private Camera myCamera = null;

        private void Update()
        {
            if (currentPlatform == null)
            {
                return;
            }
            // GET OBJECT HITTING AND POINT FACING
            Ray ray = myCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            Vector3 pointFacing = Vector3.zero;
            GameObject objectHitting = GetObjectHitting(ray, out pointFacing);
            if (objectHitting == null)
            {
                pointFacing = ray.GetPoint(512f);
            }
            Platform platform = GetPlatformFromObject(objectHitting);
            UnitMobile unitMobile = GetUnitMobileFromObject(objectHitting);

            transform.position = currentPlatform.TeleportPoint;
            if (currentPlatform.IsSensorColumnShown)
            {
                currentPlatform.IsSensorColumnShown = false;
            }
            // POINT TURRETS AT POINT FACING
            currentPlatform.PointTurretsAtPoint(pointFacing);

            if (cursor)
            {
                cursor.transform.position = pointFacing;
            }

            // IS MOUSE DOWN?
            if (Input.GetMouseButtonDown(0))
            {
                if (platform != null && platform.MyTeam == myTeam)
                {
                    Debug.Log("PLATFORM");
                    SetCurrentPlatform(platform);
                }
                else
                {
                    currentPlatform.FireTurrets();
                }
            }
        }

        private void SetCurrentPlatform (Platform platform)
        {
            if(currentPlatform != null)
            {
                currentPlatform.IsSensorColumnShown = true;
            }
            if (platform != null)
            {
                currentPlatform.IsSensorColumnShown = false;
                // SET CURRENT PLATFORM
                currentPlatform = platform;
                // MOVE PLAYER TO PLATFORM
                transform.position = platform.TeleportPoint;
            }
        }

        private GameObject GetObjectHitting (Ray ray, out Vector3 point)
        {
            point = Vector3.zero;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                point = hit.point;
                return hit.collider.gameObject;
            }

            return null;
        }

        private UnitMobile GetUnitMobileFromObject (GameObject obj)
        {
            if (obj != null && obj.GetComponent<UnitMobile>() != null)
            {
                return obj.GetComponent<UnitMobile>();
            }

            return null;

        }

        private Platform GetPlatformFromObject(GameObject obj)
        {
            if (obj != null && obj.GetComponent<Platform>() != null)
            {
                return obj.GetComponent<Platform>();
            }
            return null;
        }
    }
}
