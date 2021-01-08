using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Stripe;

namespace dotnetwebformacceptapayment
{

    public class Webhooks : System.Web.IHttpHandler
    {
        /// <summary>
        ///  localhost:8000/Webhooks.ashx 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var endpointSecret = "whsec_CEnfjmYibVGzKb66XdWdlSw06T8CONsz";
            var json = new StreamReader(context.Request.InputStream).ReadToEnd();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    context.Request.Headers["Stripe-Signature"],
                    endpointSecret
                );

                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        // look up the payment in the database and update it's state
                        // fulfill order
                        // send a customer email
                        // 
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        Console.WriteLine($"Payment Succeeded {paymentIntent.Id} for ${paymentIntent.Amount}");
                        break;
                    default:
                        Console.WriteLine($"Got event {stripeEvent.Type}");
                        break;
                }
            }
            catch (StripeException e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
