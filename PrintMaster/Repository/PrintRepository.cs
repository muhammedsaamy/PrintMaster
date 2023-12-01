using PrintMaster.Interfaces;
using Spire.Pdf;
using System.Drawing.Printing;
using Spire.Doc;
using System.Reflection.PortableExecutable;

namespace PrintMaster.Repository
{
    public class PrintRepository : IPrintRepository
    {
        public string PrintFilesInDirectory(string directoryPath, string printerName)
        {
            try
            {

                if (!PrinterExists(printerName))
                {
                    return $"Printer '{printerName}' does not exist.";
                }


                // List all files supported extensions
                string[] supportedExtensions = { ".pdf", ".doc", ".docx" };

                if (ValidateDirectoryPath(directoryPath))
                {
                    // List all files in the directory , Get all PDF and Docs files in the directory
                    string[] files = Directory.GetFiles(directoryPath)
                                              .Where(file => supportedExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                                              .ToArray();
                    if (files.Length > 0)
                    {
                        foreach (var file in files)
                        {
                            PrintFile(file, printerName);
                        }
                        return $"Printer '{printerName}' exists and printing process initiated.";
                    }
                    else
                    {
                            return "No files found in the directory.";
                    }
                }

                return $"Printer '{printerName}' exists but {directoryPath} doesn't exist.";
            }
            catch (Exception ex)
            {
                return $"Error printing files in directory {directoryPath}: {ex.Message}";
            }
        }

        public void PrintFile(string filePath, string printerName)
        {
            if (ValidateFilePath(filePath))
            {
                if (filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    PrintPdf(filePath, printerName);
                }
                else if (filePath.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                         filePath.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    PrintDoc(filePath, printerName);
                }
            }


        }

        public bool PrintPdf(string pdfFilePath, string printerName)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();

                // Load the PDF file
                pdfDocument.LoadFromFile(pdfFilePath);

                pdfDocument.PrintSettings.PrinterName = printerName;


                // Print the PDF document
                pdfDocument.Print();

                // Dispose of the PdfDocument object
                pdfDocument.Dispose();

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool PrintDoc(string docFilePath, string printerName)
        {
            try
            {
                // Create a Document object
                Document document = new Document();

                // Load the Word document
                document.LoadFromFile(docFilePath);

                //Get the PrintDocument object
                PrintDocument printDoc = document.PrintDocument;

                printDoc.DefaultPageSettings.PrinterSettings.PrinterName= printerName;

                // Print the Word document
                printDoc.Print();

                // Dispose of the Document object
                document.Dispose();

                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool PrinterExists(string printerName)
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                if (installedPrinter.Equals(printerName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static List<string> GetAvailablePrinters()
        {
            var prentersList = new List<string>();
            try
            {
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    Console.WriteLine("Printer: " + printer);
                    prentersList.Add(printer);
                }
                return prentersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting printers. Error: " + ex.Message);
                return prentersList;
            }
        }

        private static bool ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return false;
            else
                return true;
            
        }

        private static bool ValidateDirectoryPath(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
                return false;
            else 
                return true;
        }


    }
}
