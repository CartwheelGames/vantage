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
        private Platform currentPlatform;
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
        private Camera myCamera;


        private void Start ()
        {
            myCamera = gameObject.GetComponent<Camera>();
        }

        private void Update()
        {
            // GET OBJECT HITTING AND POINT FACING
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 pointFacing = Vector3.zero;
            GameObject objectHitting = GetObjectHitting(ray, out pointFacing);
            Platform platform = GetPlatformFromObject(objectHitting);
            UnitMobile unitMobile = GetUnitMobileFromObject(objectHitting);

            // POINT TURRETS AT POINT FACING
            currentPlatform.PointTurretsAtPoint(pointFacing);

            // IS MOUSE DOWN?
            if (Input.GetMouseButtonDown(0))
            {
                // IF MOBILE UNIT, FIRE! IF PLATFORM TELEPORT!
                if (unitMobile != null && unitMobile.MyTeam != myTeam)
                {
                    Debug.Log("MOVING UNIT"); 
                    currentPlatform.FireTurrets();
                }
                else if (platform != null && platform.MyTeam == myTeam)
                {
                    Debug.Log("PLATFORM");
                    SetCurrentPlatform(platform);
                }
            }
        }

        private void SetCurrentPlatform (Platform platform)
        {
            // SET CURRENT PLATFORM
            currentPlatform = platform;
            
            // MOVE PLAYER TO PLATFORM
            transform.position = platform.TeleportPoint.transform.position;
            transform.rotation = platform.TeleportPoint.transform.rotation;
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
