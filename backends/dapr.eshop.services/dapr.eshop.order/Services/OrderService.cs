using Dapr.Client;

namespace dapr.eshop.order.Services
{
    public class OrderService : IOrderService
    {
        public async Task<string> GetConfigs()
        {
            try
            {
                var text = "Fuckkkkkkkkkk";
                string SECRET_STORE_NAME = "localsecretstore";
                string STATE_STORE_NAME = "statestore";
                using var client = new DaprClientBuilder().Build();
                if (client != null)
                {
                    //var isOk = await client.CheckHealthAsync();
                    ///var secret = await client.GetBulkSecretAsync(SECRET_STORE_NAME);
                    //var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "nestedSeparator");
                    var stateStore = await client.GetStateAsync<string>(STATE_STORE_NAME, "redisHost");
                    text = $"Result: {string.Join(", ", stateStore)}";
                }
                else
                {
                    text = "client dapr null";
                }
                //Using Dapr SDK to get a secret

                return text;
            }
            catch (Exception ex)
            {
                return "Exception: " + ex.Message;
            }
        }
    }
}
