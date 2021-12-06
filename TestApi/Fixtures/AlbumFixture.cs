using System;
using System.Net.Http;

namespace TestApi.Fixtures
{
    public class AlbumFixture : IDisposable
    {
        public int TestId { get; set; }
        public string TestStr { get; private set; }
        public string Api { get; private set; }
        public HttpClient HttpClient { get; set; }
        public AlbumFixture()
        {
            TestStr = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            Api = "http://localhost:5000/api/Album";
            HttpClient = new HttpClient();
        }
        public void Dispose()
        {
            TestId = 0;
        }
    }
}
