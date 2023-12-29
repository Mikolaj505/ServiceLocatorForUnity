using UnityEngine;

namespace IoCConfig
{
    [CreateAssetMenu(menuName = "GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        public IoCConfig IoCConfig;
    }
}