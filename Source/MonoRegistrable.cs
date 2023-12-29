using UnityEngine;

namespace Services
{
    public abstract class MonoRegistrable : MonoBehaviour, IRegistrable
    {
        protected void Reset()
        {
            name = GetType().Name;
        }
    }
}
