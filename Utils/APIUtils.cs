using Newtonsoft.Json.Linq;
using RestSharp;

namespace PetStore.Utils;

public class APIUtils
{
    private RestClient restClient; 
    
    public APIUtils(string baseUrl)
    {
        restClient = new RestClient(new RestClientOptions
        {
            BaseUrl = new Uri(baseUrl)
        });
  
    }


    public RestResponse AddPet()
    {
        string filePath = DataUtils.GetTestDataFilePath();

        JObject jsonData = DataUtils.ReadJsonFromFile(filePath);

        var pet = jsonData["petTestData1"].ToObject<PetModel>();

        var request = new RestRequest("/pet", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(pet);

        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse DeletePet(int petId)
    {
        string filePath = DataUtils.GetTestDataFilePath();

        JObject jsonData = DataUtils.ReadJsonFromFile(filePath);

        var petIdToDelete = jsonData["petTestData1"]["id"].Value<int>();


        if (petIdToDelete != petId)
        {
            Console.WriteLine($"Указанный petId ({petId}) не совпадает с petId в данных JSON ({petIdToDelete}).");
            return null;
        }

        var request = new RestRequest($"/pet/{petIdToDelete}", Method.Delete);
        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse EditPet()
    {
        string filePath = DataUtils.GetTestDataFilePath();

        JObject jsonData = DataUtils.ReadJsonFromFile(filePath);

        var pet = jsonData["petTestData2"].ToObject<PetModel>();
        var request = new RestRequest($"/pet/", Method.Put);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(pet);
        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse GetPetById(int petId)
    {
        var request = new RestRequest($"/pet/{petId}", Method.Get);
        request.AddHeader("Content-Type", "application/json");
        var response = restClient.Execute(request);
        return response;

    }

    public RestResponse GetPetByStatus(string Status)
    {
        var request = new RestRequest($"/pet/{Status}", Method.Get);
        request.AddHeader("Content-Type", "application/json");
        var response = restClient.Execute(request);
        return response;

    }

    public RestResponse UploadPhoto(int petId, string filePath)
    {
        var request = new RestRequest($"/pet/{petId}/uploadImage", Method.Post);
        request.AddHeader("Content-Type", "multipart/form-data");
        request.AddFile("photo", filePath, "image/jpeg");

        var response = restClient.Execute(request);
        return response;
    }



}





