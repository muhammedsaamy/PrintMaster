namespace PrintMaster.Interfaces
{
    public interface IPrintRepository
    {
        bool PrintPdf(string filePath, string printerName);
        bool PrintDoc(string filePath, string printerName);
        void PrintFile(string filePath, string printerName);
        string PrintFilesInDirectory(string directoryPath, string printerName);

    }
}
