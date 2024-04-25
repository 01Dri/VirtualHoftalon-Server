using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using QuestPDF.Fluent;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Services;
using VirtualHoftalon_Server.Services.Interfaces;
using VirtualHoftalon_Server.Tests.MocksUtils;
using Xunit;
using Path = System.IO.Path;

namespace VirtualHoftalon_Server.Tests.IntegrationTests;

public class PDFGeneratorIntegrationTests
{

    [Fact]
    public void TestPDFGeneratorByAppointment()
    {
        Appointment mockAppointment = MockEntities.GetAppointmentMock();
        IPDFGenerator pdfGenerator = new PDFGeneratorService();
        Document document = pdfGenerator.GenerateDocument();
        Assert.NotNull(document);
    }

    [Fact]
    public void TestPDFGeneratorAppointmentSave()
    {
        const string folderPDFs = "C:\\Users\\didvg\\Desktop\\PROJETOS\\VirtualHoftalon-Server\\PDFs";
        Appointment mockAppointment = MockEntities.GetAppointmentMock();
        IPDFGenerator pdfGenerator = new PDFGeneratorService();
        pdfGenerator.SetAppointment(mockAppointment);
        Document document = pdfGenerator.GenerateDocument();
        Assert.NotNull(document);
        pdfGenerator.SaveDocument(document);
        string NamePDFFile = pdfGenerator.GetNamePDF();
        string textPDF = ExtractTextFromPDF(Path.Combine(folderPDFs, NamePDFFile));
        Assert.Contains(mockAppointment.Name, textPDF);
        Assert.Contains(mockAppointment.patient.Name, textPDF);
        Assert.Contains(mockAppointment.doctor.Name, textPDF);
        Assert.True(FindPDF(NamePDFFile, folderPDFs));
    }

    [Fact]
    public void TestFailedToSetNullApointment()
    {;
        Appointment mockAppointment = null;
        IPDFGenerator pdfGenerator = new PDFGeneratorService();
        Assert.Throws<FailedToSetAppointmentOnPDFGeneratorException>(() => pdfGenerator.SetAppointment(mockAppointment));

    }
    
    
    private bool FindPDF(string pdfName, string folder)
    {
        if (Directory.Exists(folder))
        {
            return Directory.GetFiles(folder, pdfName, SearchOption.AllDirectories).Length > 0;
        }
    
        return false;
    }
    
    private string ExtractTextFromPDF(string pathToPDF)
    {
        using (PdfReader reader = new PdfReader(pathToPDF))
        {
            StringWriter output = new StringWriter();
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                output.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i));
            }
            return output.ToString();
        }
    }
    
}