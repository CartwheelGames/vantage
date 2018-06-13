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
            GameObject objectHitting = GetObjectHitting();
            Platform platform = GetPlatformFromObject(objectHitting);
            UnitMobile unitMobile = GetUnitMobileFromObject(objectHitting);

            if (unitMobile != null && unitMobile.MyTeam != myTeam)
            {
                Debug.Log("MOVING UNIT"); 
                currentPlatform.PointTurretsAtTarget(objectHitting);
            }
            else if (platform != null && Input.GetMouseButtonDown(0))
            {
                Debug.Log("PLATFORM");
                SetCurrentPlatform(platform);
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

        private GameObject GetObjectHitting ()
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
