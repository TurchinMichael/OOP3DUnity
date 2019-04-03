
using System.IO;
using GeekBrains.Helpers;

namespace GeekBrains.Data
{
    public class StreamData : IData
    {
        private string _path;

        public void SetOptions(string path)
        {
            _path = Path.Combine(path, "Data.GeekBrains");
        }

        public void SetOptions(string name, string path)
        {
            _path = Path.Combine(path, name + ".GeekBrains");
        }

        public void Save(Player player)
        {
            using (var sw = new StreamWriter(_path))
            {
                sw.WriteLine(player.Name);
                sw.WriteLine(player.Hp);
                sw.WriteLine(player.IsVisible);
            }
        }

        public void Save(int id)
        {
            using (var sw = new StreamWriter(_path, true))
            {
               // sw.NewLine += id.ToString();

               sw.WriteLine(id);
                sw.Close();


                //UnityEngine.Debug.Log(_path.ToString());

                //sw.Flush(); //WriteLine(id);

                //sw.WriteLine(player.Hp);
                //sw.WriteLine(player.IsVisible);
            }
        }

        public Player Load()
        {
            var result = new Player();

            if (!File.Exists(_path)) return result;

            using (StreamReader streamReader = new StreamReader(_path)) // создать streamReader и исопльзовать только в области видимости
            {
                while (!streamReader.EndOfStream)
                {
                    result.Name = streamReader.ReadLine();
                    result.Hp = System.Convert.ToSingle(streamReader.ReadLine());
                    result.IsVisible = streamReader.ReadLine().TryBool();
                }
            }

            return result;
        }

        public System.Collections.Generic.List<int> LoadId()
        {
            System.Collections.Generic.List<int> result = new System.Collections.Generic.List<int>();// new Player();

            if (!File.Exists(_path)) return result;

            using (StreamReader streamReader = new StreamReader(_path)) // создать streamReader и исопльзовать только в области видимости
            {
                while (!streamReader.EndOfStream)
                {
                    result.Add(int.Parse(streamReader.ReadLine()));
                    //result.Hp = System.Convert.ToSingle(streamReader.ReadLine());
                    //result.IsVisible = streamReader.ReadLine().TryBool();
                }
                streamReader.Close();
            }

            using (var sw = new StreamWriter(_path, false))
            {
                sw.Write(string.Empty);
                sw.Close();
            }

            return result;
        }
    }
}
