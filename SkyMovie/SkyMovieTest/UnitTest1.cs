using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDbLib.Client;
using TMDbLib.Objects;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Utilities;

namespace SkyMovieTest
{
    [TestClass]
    public class UnitTest1
    {
        private TMDbClient _client = null;
       /* [TestMethod]
        public void TestGetApiKey()
        {
            Console.WriteLine(ConfigurationManager.AppSettings["ApiKey"]);
        }*/
        [TestMethod]
        public void TestConnect()
        {
            _client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);

        }
        [TestMethod]
        public void TestGetMovies()
        {
            _client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);

            Movie movie = _client.GetMovie(47964);
            Console.WriteLine("Movie name: {0}", movie.Title);
            //Console.WriteLine(movie.Images);

            //7d7ca9569a6728a3fee259e79fda3bff
        }
        [TestMethod]
        public void TestGetMoviesByName()
        {
            _client = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);

            SearchContainer<SearchMovie> results = _client.SearchMovie("Die Hard");
            
            Console.WriteLine("Got {0} of {1} results", results.Results.Count, results.TotalResults);
            foreach (SearchMovie result in results.Results)
            Console.WriteLine(result.Title);
            
 
        }
    }
}
