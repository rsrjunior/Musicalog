using Xunit;
using System.Net.Http;
using TestApi.Attributes;
using WebApi.Models;
using System.Text;
using Newtonsoft.Json;
using TestApi.Fixtures;
using System.Collections.Generic;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TestApi
{

    [TestCaseOrderer("TestApi.Orderers.PriorityOrderer", "TestApi")]
    public class AlbumEndpointsTests : IClassFixture<AlbumFixture>
    {
        private AlbumFixture _fixture;
        public AlbumEndpointsTests(AlbumFixture fixture) 
        {
            _fixture = fixture;
        }
        [Fact(DisplayName = "1. Should retrieve a list of albuns"), TestPriority(1)]
        public async void GetTest()
        {
            var result = await _fixture.HttpClient.GetAsync(_fixture.Api) ;
            Assert.True(result.IsSuccessStatusCode);
        }
        [Fact(DisplayName = "2. Should create a new album"), TestPriority(2)]
        public async void PostTest()
        {
            var result = await _fixture.HttpClient.PostAsync(_fixture.Api, 
                new StringContent(JsonConvert.SerializeObject(new AlbumModel{
                    Title = "PostTest" + _fixture.TestStr,
                    ArtistName = "PostTest" + _fixture.TestStr,
                    Type = "CD",
                    Stock = 999
                }), Encoding.UTF8, "application/json"));


            _fixture.TestId = JsonConvert.DeserializeObject<AlbumModel>(result.Content.ReadAsStringAsync().Result).Id;
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(_fixture.TestId > 0);
        }
        [Fact(DisplayName = "3. Should retrieving the new album by Id"), TestPriority(3)]
        public async void GetByIdTest()
        {
            var result = await _fixture.HttpClient.GetAsync(_fixture.Api + "/" + _fixture.TestId.ToString());
            Assert.True(result.IsSuccessStatusCode);
        }
        [Theory(DisplayName = "4. The albums must be filterable by Title and Artist Name"), TestPriority(4)]
        [InlineData("?title={title}&artistName={artistName}")]
        [InlineData("?title={title}")]
        [InlineData("?artistName={artistName}")]
        public async void GetFiltered(string query) 
        {
            string replaceWith = "PostTest" + _fixture.TestStr;
            query = query.Replace("{title}", replaceWith).Replace("{artistName}", replaceWith);
 
            var result = await _fixture.HttpClient.GetAsync(_fixture.Api + query);
            var resultData = JsonConvert.DeserializeObject<IEnumerable<AlbumModel>>(result.Content.ReadAsStringAsync().Result);
            Assert.True(result.IsSuccessStatusCode);
            Assert.NotEmpty(resultData);
        }
        [Fact(DisplayName = "5. Should edit an existing album"), TestPriority(5)]
        public async void PutTest()
        {
            var result = await _fixture.HttpClient.PutAsync(_fixture.Api + "/" + _fixture.TestId.ToString(),
               new StringContent(JsonConvert.SerializeObject(new AlbumModel
               {
                   Title = "PutTest" + _fixture.TestStr,
                   ArtistName = "PutTest" + _fixture.TestStr,
                   Type = "CD",
                   Stock = 999
               }), Encoding.UTF8, "application/json"));

            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact(DisplayName = "6. Should delete an existing album"), TestPriority(6)]
        public async void DeleteTest()
        {
            var result = await _fixture.HttpClient.DeleteAsync(_fixture.Api + "/" + _fixture.TestId.ToString());
            Assert.True(result.IsSuccessStatusCode);
        }

        
    }
}