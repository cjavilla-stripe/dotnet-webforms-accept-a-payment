<%@ Page Language="C#" Inherits="dotnetwebformacceptapayment.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
  <title>Default</title>
   <script src="https://js.stripe.com/v3/"></script>
   <script>
     document.addEventListener("DOMContentLoaded", function() {
       var stripe = Stripe("pk_test_...");

       // Set up elements
       var elements = stripe.elements();
       var cardElement = elements.create('card');
       cardElement.mount('#card-element');

       var nameInput = document.getElementById("name");

       // When the form is submitted...
       var form = document.getElementById("paymentform");
       form.addEventListener("submit", function(e) {
         e.preventDefault();

         // Tokenize payment details and confirm the payment intent.
         stripe.confirmCardPayment(
           "<%= clientSecret %>",
           {
             payment_method: {
               card: cardElement,
               billing_details: {
                 name: name.value,
               }
             }
           }
         ).then(function(result) {
           if(result.error) {
             alert(result.error.message);

           } else {
             console.log("successful payment!");

             form.submit();
           }
         })
       });
     });
   </script>
</head>
<body>
   <h1>Pay $100</h1>

   <span>PaymentIntent Client Secret: <%= clientSecret %></span>

  <form id="paymentform" runat="server" method="post">
    <input type="text" id="name" runat="server" value="jenny rosen" />
    <div id="card-element">Card element is rendered here.</div>
    <asp:Button id="button1" runat="server" Text="Pay" />
  </form>
</body>
</html>
