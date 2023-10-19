using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reqres.Utils;
using RestSharp;
using System.Net;


namespace Reqres
{
    public class Tests
    {
        private APIUtils apiUtils;

        [SetUp]

        public void SetUp()
        {
            apiUtils = new APIUtils("https://reqres.in/");

        }

        [Test]
        public void SingleUser()
        {
            string filePath = DataUtils.GetTestDataFilePath("testdataSingleUser.json");
            JObject jsonData = DataUtils.ReadJsonFromFile(filePath);
            int userId = jsonData["data"]["id"].Value<int>();
            string endpoint = $"/api/users/{userId}";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }

        [Test]
        public void ListUsers()
        {
            string filePath = DataUtils.GetTestDataFilePath("testdataListOfUsers.json");
            JObject jsonData = DataUtils.ReadJsonFromFile(filePath);
            int page = jsonData["page"].Value<int>();
            string endpoint = $"/api/users?page={page}";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Rootobject apiResponse = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            Console.WriteLine($"Page: {apiResponse.page}");
            Console.WriteLine($"Per_page: {apiResponse.per_page}");
            Console.WriteLine($"Total: {apiResponse.total}");
            Console.WriteLine($"Total pages: {apiResponse.total_pages}");
            Console.WriteLine($"Data: {apiResponse.data}");
            foreach (var user in apiResponse.data)
            {
                Console.WriteLine($"User ID: {user.id}");
                Console.WriteLine($"Email: {user.email}");
                Console.WriteLine($"First Name: {user.first_name}");
                Console.WriteLine($"Last Name: {user.last_name}");
                Console.WriteLine($"Avatar: {user.avatar}");
                Console.WriteLine();
            }



        }

        [Test]
        public void NotFoundSingleUser()
        {

            string endpoint = $"/api/users/23";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }

        [Test]
        public void ListResourse()
        {
            string endpoint = $"/api/unknown";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Rootobject apiResponse = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            Console.WriteLine($"Page: {apiResponse.page}");
            Console.WriteLine($"Per_page: {apiResponse.per_page}");
            Console.WriteLine($"Total: {apiResponse.total}");
            Console.WriteLine($"Total pages: {apiResponse.total_pages}");
            Console.WriteLine($"Data: {apiResponse.data}");
            foreach (var resourse in apiResponse.data)
            {

                Console.WriteLine($"User ID: {resourse.id}");
                Console.WriteLine($"Name: {resourse.name}");
                Console.WriteLine($"Year: {resourse.year}");
                Console.WriteLine($"Color: {resourse.color}");
                Console.WriteLine($"pantone_value: {resourse.pantone_value}");
                Console.WriteLine();
            }

        }

        [Test]
        public void SingleResourse()
        {
            string endpoint = $"/api/unknown/2";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }


        [Test]
        public void NotFoundSingleResourse()
        {
            string endpoint = $"/api/unknown/23";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }

        [Test]
        public void Create()
        {
            string filePath = DataUtils.GetTestDataFilePath("testdataPostUser.json");

            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/users/";
            RestResponse response = apiUtils.PostUserData(endpoint, requestBody);
            Assert.AreEqual((int)HttpStatusCode.Created, (int)response.StatusCode);
            Console.WriteLine("Участник успешно добавлен.");
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);
        }

        [Test]
        public void UpdatePut()
        {
            string filePath = DataUtils.GetTestDataFilePath("Update(Put).json");
            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/users/2";
            RestResponse response = apiUtils.EditUserData(endpoint, requestBody);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }

        [Test]
        public void UpdatePatch()
        {
            string filePath = DataUtils.GetTestDataFilePath("Update(Put).json");
            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/users/2";
            RestResponse response = apiUtils.EditUserData(endpoint, requestBody);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

        }


        [Test]
        public void DeleteContent()
        {
            string endpoint = $"/api/users/2";
            RestResponse response = apiUtils.DeleteUserData(endpoint);
            Assert.AreEqual((int)HttpStatusCode.NoContent, (int)response.StatusCode);
            Console.WriteLine("Запрос успешно выполнен, и сервер не вернул контент (No Content).");


        }

        [Test]
        public void SuccessfulRegister()
        {
            string filePath = DataUtils.GetTestDataFilePath("RegisterSucsessful.json");

            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/register/";
            RestResponse response = apiUtils.PostUserData(endpoint, requestBody);
            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Console.WriteLine("Участник успешно добавлен.");
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

            JObject jsonResponse = JObject.Parse(response.Content);
            Assert.IsTrue(jsonResponse.ContainsKey("id"), "Отсутствует 'id' в ответе.");
            Assert.IsTrue(jsonResponse.ContainsKey("token"), "Отсутствует 'token' в ответе.");
        }

        [Test]
        public void UnSuccessfulRegister()
        {
            string filePath = DataUtils.GetTestDataFilePath("RegisterUnSucsessful.json");

            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/register/";
            RestResponse response = apiUtils.PostUserData(endpoint, requestBody);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            Console.WriteLine("Участник успешно добавлен.");
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

            JObject jsonResponse = JObject.Parse(response.Content);
            Assert.IsTrue(jsonResponse.ContainsKey("error"));

        }


        [Test]
        public void SuccessfulLogin()
        {
            string filePath = DataUtils.GetTestDataFilePath("LoginSucsessful.json");

            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/login/";
            RestResponse response = apiUtils.PostUserData(endpoint, requestBody);
            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode); Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);

            JObject jsonResponse = JObject.Parse(response.Content);
            Assert.IsTrue(jsonResponse.ContainsKey("token"));
        }


        [Test]
        public void UnSuccessfulLogin()
        {
            string filePath = DataUtils.GetTestDataFilePath("LoginUnSucsessful.json");

            string requestBody = File.ReadAllText(filePath);
            string endpoint = $"/api/login/";
            RestResponse response = apiUtils.PostUserData(endpoint, requestBody);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
            JObject jsonResponse = JObject.Parse(response.Content);
            Assert.IsTrue(jsonResponse.ContainsKey("error"));
            Console.WriteLine("Ответ сервера: " + response.Content);
        }

        [Test]
        public void DelayedReesponse()
        {
            string endpoint = $"/api/users?delay=3";
            RestResponse response = apiUtils.GetUserData(endpoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Rootobject apiResponse = JsonConvert.DeserializeObject<Rootobject>(response.Content);


            Console.WriteLine("Ответ сервера: " + response.Content);
            Console.WriteLine($"Page: {apiResponse.page}");
            Console.WriteLine($"Per_page: {apiResponse.per_page}");
            Console.WriteLine($"Total: {apiResponse.total}");
            Console.WriteLine($"Total pages: {apiResponse.total_pages}");
            Console.WriteLine($"Data: {apiResponse.data}");
            foreach (var user in apiResponse.data)
            {
                Console.WriteLine($"User ID: {user.id}");
                Console.WriteLine($"Email: {user.email}");
                Console.WriteLine($"First Name: {user.first_name}");
                Console.WriteLine($"Last Name: {user.last_name}");
                Console.WriteLine($"Avatar: {user.avatar}");
         
            }



        }




    }
     



    }

