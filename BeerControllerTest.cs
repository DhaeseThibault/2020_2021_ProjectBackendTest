using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Testing;

using FluentAssertions;

using Newtonsoft.Json;

using ProjectBackend.DTO;
using ProjectBackend.Models;

using Xunit;

namespace ProjectBackendTest
{
    public class BeerControllerTest : IClassFixture<WebApplicationFactory<ProjectBackend.Startup>>
    {
        public HttpClient Client { get; set; }

        public BeerControllerTest(WebApplicationFactory<ProjectBackend.Startup> fixture)
        {
            Client = fixture.CreateClient();    
        }


        [Fact]
        public async Task Get_Brewers_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/brewers");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var brewers = JsonConvert.DeserializeObject<List<BrewerDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(brewers.Count > 0);
        }

        [Fact]
        public async Task Get_Beers_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/beers");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(beers.Count > 0);
        }

        [Fact]
        public async Task Get_Specific_Beer_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/beer/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var beer = JsonConvert.DeserializeObject<Beer>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(beer);
        }

        // [Fact]
        // public async Task Get_Users_Should_Return_Ok()
        // {
        //     var response = await Client.GetAsync("/api/users");
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);

        //     var brewers = JsonConvert.DeserializeObject<List<UserDTO>>(await response.Content.ReadAsStringAsync());
        //     Assert.True(brewers.Count > 0);
        // }

        // [Fact]
        // public async Task Add_Beers_Test()
        // {
        //     var beer = new BeerDTO()            
        //     {
        //         Name = "Carlsburg",
        //         Percentage = "5.2",
        //         Origin = "HupHupHolland",
        //         BitternessId = 1,
        //         BrewerId = 1,
        //         Users = new List<int>()
        //     };
        //     string json = JsonConvert.SerializeObject(beer);
        //     var response = await Client.PostAsync("/api/beers", new StringContent(json, Encoding.UTF8, "application/json"));
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);

        //     var createdBeer = JsonConvert.DeserializeObject<BeerDTO>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(createdBeer);
        // }
    }
}
