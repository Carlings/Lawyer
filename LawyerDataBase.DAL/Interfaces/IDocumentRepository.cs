using Common;
using LawyerDataBase.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerDataBase.DAL.Interfaces
{
    public interface IDocumentRepository
    {
        public Result<Document> AddDocument(Document document);
        public Result<Document> GetByID(int id);
        public Result<IQueryable<Document>> GetAll();
        public Result<IQueryable<Document>> Search(string searchTerm);
        public Result<Document> Delete(int id);
    }
}
