using UnityEngine;

namespace TwilightRun
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private Transform _followedObject;
        [SerializeField] private float _offset;

        private void LateUpdate()
        {
            transform.position = new Vector3(_followedObject.position.x + _offset, transform.position.y, transform.position.z);
        }
    }

}