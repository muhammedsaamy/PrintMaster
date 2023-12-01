# PrintMaster
PrintMaster is designed to simplify and streamline the process of printing multiple files within a directory. Whether you need to batch print documents or create an efficient printing workflow, PrintMaster offers a user-friendly solution.
Enhance the PrintFiles endpoint in the PrintingController with the following improvements:
1. **Input Validation:**
   Validate that DirectoryPath and PrinterName are provided in the request. Return a 400 Bad Request if the input is invalid.
2. **Logging:**
   Use the logging framework to log errors and success information. Include more context in log messages for debugging and monitoring purposes.
3. **Response Types:**
   Return appropriate HTTP status codes and response messages. Include the returned message from the service in the Ok response for more information about the success.
4. **Dependency Injection:**
   Ensure that _printingService and _logger are injected into the controller through the constructor for better maintainability and testability.
5. **Contextual Log Messages:**
   Include more context in log messages, making it easier to understand issues during debugging or monitoring.
