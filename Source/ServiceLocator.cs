using IoCConfig;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    /// <summary>
    /// Main class of ServiceLocator implementation
    /// </summary>
    public abstract class ServiceLocator : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Clear()
        {
            Services.Clear();
        }

        public static void Register<TServiceKey>(IRegistrable service, bool safe = true)
        {
            CheckRegistrationSafety<TServiceKey>(safe);
            RegisterInternally<TServiceKey>(service);
        }

        public static void RegisterFromIoC<TServiceKey>(bool safe = true)
        {
            CheckRegistrationSafety<TServiceKey>(safe);
            if (GameConfigHelper.IsGameConfigLoaded == false)
            {
                throw new ServiceLocatorException($"Couldn't register {typeof(TServiceKey).Name} from IoC because game config has not been loaded");
            }
            IRegistrable instance = GameConfigHelper.IoCConfig?.GetObjectForKeyType<TServiceKey>();
            if(instance == null)
            {
                throw new ServiceLocatorException($"{typeof(TServiceKey).Name} not found in IoC config or config is null");
            }
            RegisterInternally<TServiceKey>(instance);
        }

        private static void RegisterInternally<TServiceKey>(IRegistrable service)
        {
            Services[typeof(TServiceKey)] = service;
        }

        private static void CheckRegistrationSafety<TServiceKey>(bool safe)
        {
            if (IsRegistered<TServiceKey>() && safe)
            {
                throw new ServiceLocatorException($"{typeof(TServiceKey).Name} has been already registered.");
            }
        }

        public static void Unregister<TServiceKey>()
        {
            var serviceType = typeof(TServiceKey);
            if (IsRegistered<TServiceKey>() == false)
            {
                return;
            }

            Services.Remove(serviceType);
        }

        public static TServiceKey Get<TServiceKey>()
        {
            var serviceType = typeof(TServiceKey);
            if (IsRegistered<TServiceKey>())
            {
                return (TServiceKey)Services[serviceType];
            }

            throw new ServiceLocatorException($"{serviceType.Name} hasn't been registered.");
        }

        public static bool IsRegistered(Type t)
        {
            return Services.ContainsKey(t);
        }

        public static bool IsRegistered<TServiceKey>()
        {
            return IsRegistered(typeof(TServiceKey));
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message)
        {
        }
    }
}