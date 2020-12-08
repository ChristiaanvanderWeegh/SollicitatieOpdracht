namespace Client.Models
{
    public class OAuthSecretResponse
    {
        public OAuthSecretResponse(string serverResponse, string apiResponse)
        {
            ServerResponse = serverResponse;
            ApiResponse = apiResponse;
        }
        public string ServerResponse { get; }
        public string ApiResponse { get; }
    }
}
