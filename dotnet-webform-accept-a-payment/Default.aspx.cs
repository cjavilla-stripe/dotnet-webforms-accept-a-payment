using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Stripe;

namespace dotnetwebformacceptapayment
{

    public partial class Default : System.Web.UI.Page
    {
        public string clientSecret = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Create a payment intent, expose it's client secret publicly.
                var options = new PaymentIntentCreateOptions
                {
                    Amount = 10000,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                };
                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);
                clientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Handle post request from form submission
                // Put payment information in the database
            }
        }
    }
}
