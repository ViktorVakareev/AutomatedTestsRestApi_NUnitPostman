using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace AutomatedTestsRestApi_NUnit
{
    public class ShortUrlApi_NUnitTests
    {
        const string ShortUrlHome = "https://shorturl.viktorvakareev.repl.co/api";
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient(ShortUrlHome);
            client.Timeout = 3000;
        }

        [Test]
        public void Test1_ListShortUrls()
        {

            var request = new RestRequest("/urls", Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var urls = new JsonDeserializer().Deserialize<List<UrlResponse>>(response);

            Assert.IsTrue(urls != null);
        }

        [Test]
        public void Test2_FindUrlByShortCode_ValidInput()
        {

            var request = new RestRequest("/urls/seldev", Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var expectedUrl = new UrlResponse
            {
                url = "https://selenium.dev",
                shortCode = "seldev",
                shortUrl = "http://shorturl.viktorvakareev.repl.co/go/seldev",
                dateCreated = "2021-02-17T22:07:08.000Z",
                visits = 43
            };
            var expectedUrlJSON = new JsonDeserializer().Serialize(expectedUrl);
            var urlJSON = response.Content;
            ;
            Assert.AreEqual(expectedUrlJSON, urlJSON);

        }
        [Test]
        public void Test2_FindUrlByShortCode_InvalidInput()
        {

            var request = new RestRequest("/urls/InvalidInput", Method.GET);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
        [Test]
        public void Test3_CreateNewShortCode_ValidInput()
        {

            var request = new RestRequest("/urls", Method.POST);
                      

            request.AddHeader("Content-Type", "application/json");
            string newCode = "sof" + DateTime.Now.Ticks;
            var newUrl = new
            {
                url = "https://stackoverflow.com/questions",
                shortCode = newCode
            };
            request.AddJsonBody(newUrl);

            var response = client.Execute(request);
            var urlResponse = new JsonDeserializer().Deserialize<CreateUrlResponse>(response);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
           
            Assert.AreEqual("Short code added.", urlResponse.msg);
            Assert.AreEqual(0, urlResponse.url.visits);
            Assert.AreEqual(newUrl.url, urlResponse.url.url);
            Assert.AreEqual(newUrl.shortCode, urlResponse.url.shortCode);
        }
        [Test]
        public void Test3_CreateNewShortCode_InvalidInput()
        {

            var request = new RestRequest("/urls", Method.POST);


            request.AddHeader("Content-Type", "application/json");
            string newCode = "sof" + DateTime.Now.Ticks;
            var newUrl = new
            {
                url = "InvalidURL",
                shortCode = newCode
            };
            request.AddJsonBody(newUrl);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

            [Test]
        public void Test3_DeleteNewShortCode_ValidInput()
        {
            // 1. Create new URL
            var request = new RestRequest("/urls", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string newCode = "del" + DateTime.Now.Ticks;
            var newUrl = new
            {
                url = "https://deleteme.com",
                shortCode = newCode
            };
            request.AddJsonBody(newUrl);
            var response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Delete the created URL
            var delRequest = new RestRequest("/urls/"+ newUrl.shortCode, Method.DELETE);
            var delResponse = client.Execute(delRequest);

            Assert.AreEqual(HttpStatusCode.OK, delResponse.StatusCode);     
           
        }
        [Test]
        public void Test3_DeleteNewShortCode_InvalidInput()
        {
                               
            // Delete the invalid URL
            var delRequest = new RestRequest("/urls/InvalidURL" , Method.DELETE);
            var delResponse = client.Execute(delRequest);

            Assert.AreEqual(HttpStatusCode.NotFound, delResponse.StatusCode);
        }
        [Test]
        public void Test3_VisitShortCode_IncreaseVisitCount_ValidInput()
        {
           
            var request = new RestRequest("/urls/visit/nak", Method.POST);
           
            var response1 = client.Execute(request);
            var response2 = client.Execute(request);

            var urlResponse1 = new JsonDeserializer().Deserialize<UrlResponse>(response1);
            var urlResponse2 = new JsonDeserializer().Deserialize<UrlResponse>(response2);

            Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
            Assert.AreEqual(1, urlResponse2.visits-urlResponse1.visits);

        }
        [Test]
        public void Test3_VisitShortCode_IncreaseVisitCount_InvalidInput()
        {
            var request = new RestRequest("/urls/visit/invalid", Method.POST);
            var response = client.Execute(request);          

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

    }

    public class CreateUrlResponse
    {

        public string msg { get; set; }
        public UrlResponse url { get; set; }
    }

    public class UrlResponse
    {
        public string url { get; set; }
        public string shortCode { get; set; }
        public string shortUrl { get; set; }
        public string dateCreated { get; set; }
        public int visits { get; set; }

    }
}