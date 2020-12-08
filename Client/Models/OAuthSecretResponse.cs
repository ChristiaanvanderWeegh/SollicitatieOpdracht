namespace Client.Models
{
    public class OAuthSecretResponse
    {
        public OAuthSecretResponse(string serverResponse)
        {
            ServerResponse = serverResponse;
        }
        public string ServerResponse { get; }
    }
}
