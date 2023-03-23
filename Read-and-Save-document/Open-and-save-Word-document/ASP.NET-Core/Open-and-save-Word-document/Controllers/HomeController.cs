﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Open_and_save_Word_document.Models;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace Open_and_save_Word_document.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult OpenAndSaveDocument()
        {
            //Open the file as Stream.
            using (FileStream docStream = new FileStream(@"Data/Input.docx", FileMode.Open, FileAccess.Read))
            {
                //Load the file stream into a Word document.
                using (WordDocument document = new WordDocument(docStream, FormatType.Docx))
                {
                    //Access the section in a Word document.
                    IWSection section = document.Sections[0];
                    //Add new paragraph to the section.
                    IWParagraph paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.FirstLineIndent = 36;
                    paragraph.BreakCharacterFormat.FontSize = 12f;
                    //Add new text to the paragraph.
                    IWTextRange textRange = paragraph.AppendText("In 2000, AdventureWorks Cycles bought a small manufacturing plant, Importadores Neptuno, located in Mexico. Importadores Neptuno manufactures several critical subcomponents for the AdventureWorks Cycles product line. These subcomponents are shipped to the Bothell location for final product assembly. In 2001, Importadores Neptuno, became the sole manufacturer and distributor of the touring bicycle product group.") as IWTextRange;
                    textRange.CharacterFormat.FontSize = 12f;

                    //Save the Word document to MemoryStream.
                    MemoryStream stream = new MemoryStream();
                    document.Save(stream, FormatType.Docx);
                    stream.Position = 0;

                    //Download Word document in the browser.
                    return File(stream, "application/msword", "Sample.docx");

                }
            }
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
