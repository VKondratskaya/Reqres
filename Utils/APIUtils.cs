
using RestSharp;


namespace Reqres.Utils;

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




    public RestResponse DeleteUserData(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Delete);
        request.AddHeader("Content-Type", "application/json");
        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse EditUserData(string endpoint, string requestBody)
    {
        var request = new RestRequest(endpoint, Method.Put);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse UpdateUserData(string endpoint, string requestBody)
    {
        var request = new RestRequest(endpoint, Method.Patch);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
        var response = restClient.Execute(request);
        return response;
    }


    public RestResponse GetUserData(string endpoint)
    {
         var request = new RestRequest(endpoint, Method.Get);
        request.AddHeader("Content-Type", "application/json");
        var response = restClient.Execute(request);
        return response;
    }

    public RestResponse PostUserData(string endpoint, string requestBody)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

        var response = restClient.Execute(request);
        return response;
    }






}





