using UnityEngine;

namespace ScriptsMisha.Components.Weapon
{
    public class WeaponForceMove : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] float Forse;
    
        [Space]
        [SerializeField] bool InversX, InversZ;
    
        private void Update()
        {
            float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");
    
            float inversX = InversX ? -1 : 1;
            float inversZ = InversZ ? -1 : 1;
    
            Vector3 _Temp = new Vector3(x * Forse * inversX,  -0.56f, z * Forse * inversZ);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _Temp, 1);
        }
    }
}
