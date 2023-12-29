using System;
using System.Linq;
using JetBrains.Annotations;
using Services;
using UnityEngine;

namespace IoCConfig
{
    [Serializable]
    [CreateAssetMenu(menuName = IoCStaticStrings.CONFIG_PATH + "/MainConfig", fileName = "IoCConfig")]
    public class IoCConfig : ScriptableObject
    {
        [SerializeField] private IoCTypeConfigBase[] _ioCTypeConfigs;

        [CanBeNull]
        public IRegistrable GetObjectForKeyType<TKeyType>()
        {
            foreach (var config in _ioCTypeConfigs)
            {
                if (config.GetKeyType() == typeof(TKeyType))
                {
                    var registrable = config.GetObjectForType();
                    return registrable;
                }
            }

            return null;
        }
    }
}