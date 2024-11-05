using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class Sway : MonoBehaviour
    {
        [SerializeField,Min(0)] Vector2 Forse = Vector2.one;
        [SerializeField, Min(0)] float Speed = 3;
        [SerializeField] bool InverseX, InverseY;

        [Header("Min&Max")]
        [SerializeField] Vector2 MinMaxX = Vector2.one;
        [SerializeField] Vector2 MinMaxY = Vector2.one;

        private void LateUpdate()
        {
            float inverseX = InverseX ? -1 : 1;
            float inverseY = InverseY ? -1 : 1;

            float MX = Input.GetAxis("Mouse X") * Forse.x;
            float MY = Input.GetAxis("Mouse Y") * Forse.y;

            MX = Mathf.Clamp(MX, MinMaxX.x, MinMaxX.y);
            MY = Mathf.Clamp(MY, MinMaxY.x, MinMaxY.y);

            var _temp = transform.localEulerAngles;

            _temp.x = Mathf.LerpAngle(_temp.x, MY * inverseY, Speed * Time.deltaTime);
            _temp.y = Mathf.LerpAngle(_temp.y, MX * inverseX, Speed * Time.deltaTime);

            transform.localEulerAngles = _temp;

        }
    }
}
