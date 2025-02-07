using UnityEngine;

namespace NotsGame.Core
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = Constants.COMPANY_NAME + "/Data/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        private GameConfig _instance;

        public GameConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameConfig();
                }
                return _instance;
            }
        }
    }
}