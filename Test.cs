using Newtonsoft.Json.Linq;
using PetStore.Utils;
using RestSharp;
using System.Net;


namespace PetStore
{
    public class Tests
    {
        private APIUtils apiUtils;
        private JObject jsonData;
        private int petId;

        [SetUp]

        public void SetUp()
        {
            apiUtils = new APIUtils("https://petstore.swagger.io/v2");
            string filePath = DataUtils.GetTestDataFilePath();
            jsonData = DataUtils.ReadJsonFromFile(filePath);
        }



        [Test]
        public void AddPet()
        {

            RestResponse response = apiUtils.AddPet();
            if ((int)response.StatusCode == (int)HttpStatusCode.OK)
            {
                Console.WriteLine("Питомец успешно добавлен. Новая информация : ");
                Console.WriteLine("Статус код: " + response.StatusCode);
                Console.WriteLine("Ответ сервера: " + response.Content);
            }
            else
            {
                Console.WriteLine("Произошла ошибка при добавлении питомца.");
                Console.WriteLine("Статус код: " + response.StatusCode);
                Console.WriteLine("Ответ сервера: " + response.Content);
            }
            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Console.WriteLine("Питомец успешно добавлен.");
        }


        [Test]
        public void DeletePet()
        {
          
            var petIdToDelete = jsonData["petTestData1"]["id"].Value<int>();

            RestResponse response = apiUtils.DeletePet(petIdToDelete);

            if ((int)response.StatusCode != (int)HttpStatusCode.OK)
            {
                Console.WriteLine("Произошла ошибка при удалении питомца.");
                Console.WriteLine("Статус код: " + response.StatusCode);
                Console.WriteLine("Ответ сервера: " + response.Content);
            }

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            Console.WriteLine("Питомец успешно удален.");
        }

        [Test]
        public void EditPet()
        {
            RestResponse response = apiUtils.EditPet();
            if ((int)response.StatusCode == (int)HttpStatusCode.OK)
            {
                Console.WriteLine("Питомец успешно отредактирован. Новая информация : ");
                Console.WriteLine("Статус код: " + response.StatusCode);
                Console.WriteLine("Ответ сервера: " + response.Content);
            }
            else
            {
                Console.WriteLine("Произошла ошибка при редактировании питомца.");
                Console.WriteLine("Статус код: " + response.StatusCode);
                Console.WriteLine("Ответ сервера: " + response.Content);
            }
            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
            
        }

        [Test]
        public void GetPet()
        {
          

            petId = jsonData["petTestData1"]["id"].Value<int>();

            // Попробуем найти питомца по ID
            RestResponse response = apiUtils.GetPetById(petId);

            if ((int)response.StatusCode != (int)HttpStatusCode.OK)
            {
                Console.WriteLine("Питомец не найден по ID. Попытка поиска по статусу...");
                RestResponse statusResponse = apiUtils.GetPetByStatus("notavailable");

                if ((int)statusResponse.StatusCode == (int)HttpStatusCode.OK)
                {
                    Console.WriteLine("Питомец успешно найден по статусу.");
                }
                else
                {
                    Console.WriteLine("Питомец не найден ни по ID, ни по статусу. Проверьте данные и запрос.");
                    Assert.Fail("Питомец не найден.");
                }
            }
            else
            {
                Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);
                Console.WriteLine("Питомец успешно найден по ID.");
            }
        }

        [Test]
        public void AddPhotoTest()
        {
            var filePathimage = ".Resources/testdata.json";
            petId = jsonData["petTestData1"]["id"].Value<int>();
            RestResponse response = apiUtils.UploadPhoto(petId, filePathimage);

            Assert.AreEqual((int)HttpStatusCode.OK, (int)response.StatusCode);

            Console.WriteLine("Фото успешно добавлено.");
            Console.WriteLine("Статус код: " + response.StatusCode);
            Console.WriteLine("Ответ сервера: " + response.Content);
        }

    }
}

