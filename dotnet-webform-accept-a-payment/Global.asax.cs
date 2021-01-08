using System.Web;
using Stripe;

namespace dotnetwebformacceptapayment
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            StripeConfiguration.ApiKey = "sk_test_...";
        }
    }
}
