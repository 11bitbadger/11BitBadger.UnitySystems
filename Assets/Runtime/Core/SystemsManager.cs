using UnityEngine;

namespace Badger.Systems
{
    [DefaultExecutionOrder(100)]
    public class SystemsManager : UnitySingleton<SystemsManager>
    {
        /// <summary>
        /// Here we ensure all children are enabled and their systems have
        /// been enabled
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            foreach (var child in GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Now we have started, we can go through and call resolve systems
        /// This allows all systems to setup inter system dependencies
        /// </summary>
        protected void Start()
        {
            foreach (var child in GetComponentsInChildren<Transform>(true))
            {
                foreach (var unitySingleton in child.GetComponents<IUnitySingleton>())
                {
                    unitySingleton.ResolveSystems();
                }
            }
        }
    }
}
