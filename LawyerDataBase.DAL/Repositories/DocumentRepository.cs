using Common;
using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LawyerDataBase.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly LawyerDataBaseContext _db;
        public DocumentRepository(LawyerDataBaseContext db)
        {
            _db = db;
        }
        public Result<Document> AddDocument(Document document)
        {
            try
            {
                _db.Documents.Add(document);
                _db.SaveChanges();

                return new Result<Document>
                {
                    Data = document,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new Result<Document>
                {
                    Data = document,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public Result<Document> Delete(int id)
        {
            var document = _db.Documents.Where(doc => doc.Id == id).FirstOrDefault();

            if (document != null)
            {
                _db.Documents.Remove(document);
                _db.SaveChanges();

                return new Result<Document>
                {
                    Success = true,
                    Data = document
                };
            }
            else
                return new Result<Document>
                {
                    Success = false,
                    Data = document
                };
        }

        public Result<IQueryable<Document>> GetAll()
        {
            try
            {
                var documents = _db.Documents.AsQueryable();

                return new Result<IQueryable<Document>>
                {
                    Data = documents,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new Result<IQueryable<Document>>
                {
                    Success = false,
                    Message= ex.Message
                };
            }
        }

        public Result<Document> GetByID(int id)
        {
            try
            {
                var document = _db.Documents.AsQueryable()
                                             .Where(obj => obj.Id == id)
                                             .FirstOrDefault();

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
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public Result<IQueryable<Document>> Search(string searchTerm)
        {
            var formattedSearchTerm = $@"""{searchTerm}""";
            try
            {   //.FromSqlInterpolated($@"SELECT * FROM Documents WHERE MATCH(Data) AGAINST({searchTerm} IN BOOLEAN MODE)")
                var results = _db.Documents
                                 .FromSqlInterpolated($@"SELECT * FROM Documents WHERE MATCH(Data) AGAINST({formattedSearchTerm} IN BOOLEAN MODE)")
                                 .AsQueryable();

                if(results != null)
                {
                    return new Result<IQueryable<Document>>
                    {
                        Data = results,
                        Success = true
                    };
                }
                else
                    return new Result<IQueryable<Document>>
                    {
                        Success = false,
                        Message = "No elements in sequence"
                    };
            }
            catch (Exception ex)
            {
                return new Result<IQueryable<Document>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
