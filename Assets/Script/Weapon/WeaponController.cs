using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private GameObject _camera;

        public int pelletsNumber = 6;
        public float spreadAngle = 40f;
        private float range = 100f;

        private void Awake()
        {
            _camera = GameObject.Find("MainCamera");
        }

        public void FixedUpdate()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                for (int pelletCount = 0; pelletCount < pelletsNumber; pelletCount++)
                {
                    ShotgunRay();
                }
            }
        }

        public void ShotgunRay()
        {
            RaycastHit pelletHit;
            GameObject target;

            Vector3 direction = _camera.transform.forward; // your initial aim.
            Vector3 spread = Vector3.zero;
            spread += _camera.transform.up * Random.Range(-1f, 1f);    // add random up or down (because random can get negative too)
            spread += _camera.transform.right * Random.Range(-1f, 1f); // add random left or right

            // Using random up and right values will lead to a square spray pattern. If we normalize this vector, we'll get the spread direction, but as a circle.
            // Since the radius is always 1 then (after normalization), we need another random call.
            direction += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(_camera.transform.position, direction, out pelletHit, range))
            {
                target = pelletHit.transform.gameObject;
                StartCoroutine(changeTargetColor(target));
                Debug.DrawLine(_camera.transform.position, pelletHit.point, Color.green, 3f);
            }
            else
            {
                Debug.DrawLine(_camera.transform.position, pelletHit.point, Color.red, 3f);
            }
        }

        private IEnumerator changeTargetColor(GameObject target)
        {
            MeshRenderer targetMesh = target.GetComponent<MeshRenderer>();
            BaseColor colorProperty = target.GetComponent<BaseColor>();
            bool hasBaseColor = colorProperty != null ? true : false;

            targetMesh.material.color = Color.yellow;
            yield return new WaitForSecondsRealtime(2);

            if (hasBaseColor)
            {
                targetMesh.material.color = colorProperty.baseColor;
            }
            else
            {
                targetMesh.material.color = Color.grey;
            }
        }
    }
}
