using Common;
using HtmlAgilityPack;
using Lawyer.BLL.Interfaces;
using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using System.Net;

namespace Lawyer.BLL.Services
{
    public class HtmlService : IHTMLService
    {
        private readonly IDocumentRepository _documentRepository;
        public HtmlService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public Result<Document> addHTMLDocument(Stream stream, int userID, string title)
        {
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.Load(stream);

                string data = WebUtility.HtmlDecode(htmlDocument.DocumentNode.InnerText);
                var document = new Document
                {
                    Title = title,
                    Content = htmlDocument.Text,
                    Data = data,
                    DateOfCreation = DateTime.Now,
                    UserID = userID
                };

                _documentRepository.AddDocument(document);

                return new Result<Document>
                {
                    Data = document,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new Result<Document>
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
           

        }
    }
}
