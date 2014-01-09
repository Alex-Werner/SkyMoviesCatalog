using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SkyMovie.Model
{
    class Persistance
    {
        private bool isFileExist = false;
        private string filename = "undefined";

        ObservableCollection<Movie> MovieDB;

        public Persistance(string _filename)
        {
            this.filename = _filename;
            if(File.Exists(filename))
            {
                this.isFileExist = true;
            }
            else
            {
                var streamW = File.Create(_filename);

                var serialiser = new BinaryFormatter();
                serialiser.Serialize(streamW, new ObservableCollection<Movie>());
                streamW.Close();

            }
        }
       
        public void setPersistance(string _filename, ObservableCollection<Movie> _collection )
        {
            File.Delete(_filename);
            var streamW = File.Create(_filename);
            var serialiser = new BinaryFormatter();
            serialiser.Serialize(streamW, _collection);
            streamW.Close();
        }
        public ObservableCollection<Movie> getPersistance(string _filename)
        {
            ObservableCollection<Movie> serializedMovieDB;
            var streamR = File.OpenRead("MovieDB.bin");
            var unserialiser = new BinaryFormatter();
            if (streamR.Length == 0)
            {
                serializedMovieDB = new ObservableCollection<Movie>();
                
                
            }
            else
            {
               serializedMovieDB = (ObservableCollection<Movie>)unserialiser.Deserialize(streamR);
                
            }
            streamR.Close();
            return serializedMovieDB;
        }
    }
}
