using UnityEngine;

namespace GeekBrains.Data
{
    public class TestData : MonoBehaviour
    {
        private StreamData _data; // : IData
        
        private void Start()
        {
            _data = new StreamData();


            var path = Application.dataPath;
            //Debug.Log(Application.dataPath);


            var player = new Player
            {
                Name = "Roman",
                Hp = 1000,
                IsVisible = true
            };

          //  Debug.Log(player.ToString());

            _data?.SetOptions(path);
            _data?.Save(player);
            var newPlayer = _data?.Load();

          //  Debug.Log(newPlayer.ToString());
        }
    }
}