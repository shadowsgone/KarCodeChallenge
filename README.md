# KarCodeChallenge

This project includes 1 Unit Testing client.
  The client runs though a deposit, withdraw and transfer for account types Checking, Savings, Individual Investment and Corporate Investment.
  We could improve upon this by having each micro-service contain its own unit testing project as this would help test each endpoint more effectively, this also would allow us to have pipelines setup to run those test projects before allowing code to be deployed to DL (Dev) or even to production.
  
This project also includes 4 Micro-Services.
   Each miro-service has a specific business object type to manage. This allows us to make smaller more frequent updates across our platform.  Some things to note is that the services are very small and need better handling around errors, object mapping as we really should not be using our DB objects to be sent over the wire, general validation on all requests, and most importantly authorization to prevent unwanted users or machines from accessing our services.
