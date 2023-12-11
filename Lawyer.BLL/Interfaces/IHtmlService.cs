using Common;
using LawyerDataBase.DAL.Entities;

namespace Lawyer.BLL.Interfaces
{
    public interface IHTMLService
    {
        Result<Document> addHTMLDocument(Stream stream, int UserID, string title);
    }
}
