using System;
using Services;
using UnityEngine;

namespace IoCConfig
{
    [Serializable]
    public abstract class IoCTypeConfigBase : ScriptableObject
    {
        public abstract Type GetKeyType();

        public abstract IRegistrable GetObjectForType();
    }
}
   