using Microsoft.AspNetCore.Mvc;
using ReadingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReadingList.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Get all neutral books (acts as GetAll() upon app startup and after refreshing rate books page)
        [Route("api/books/")]
        [HttpGet]
        public List<Book> GetAllNeutral()
        {
            return BookManager.Singleton.GetNeutralBooks();
        }

        [Route("api/books/shelvedBooks")]
        [HttpGet]
        public List<Book> GetShelvedBooks()
        {
            return BookManager.Singleton.GetShelvedBooks();
        }

        // Get user-specified book from my shelf
        [Route("api/books/shelvedBook/{id}")]
        [HttpGet]
        public Book GetShelvedBook(string id)
        {
            long idAsLong = 0;
            if (!long.TryParse(id, out idAsLong))
            {
                throw new HttpListenerException(404, "Invalid id");
            }

            Book book = BookManager.Singleton.GetShelvedBook(idAsLong);

            if (book == null)
            {
                throw new HttpListenerException(404, $"Book not found for id {id}");
            }

            return book;
        }

        [Route("api/books/neutralBook")]
        [HttpGet]
        // Get a random book from the neutral books list
        public Book GetNeutralBook()
        {
            if (!BookManager.Singleton.NeutralIsEmpty())
            {
                return BookManager.Singleton.GetNeutralBook(); 
            }
            else
            {
                return null;
            }
        }

        [Route("api/books/neutralIsEmpty")]
        [HttpGet]
        public bool NeutralIsEmpty()
        {
            return BookManager.Singleton.NeutralIsEmpty();
        }

        [Route("api/books/addShelved/{id}")]
        [HttpPut]
        public void AddShelvedBook(long id)
        {
            BookManager.Singleton.AddShelvedBook(id);
        }

        [Route("api/books/addRejected/{id}")]
        [HttpPut]
        public void AddRejectedBook(long id)
        {
            BookManager.Singleton.AddRejectedBook(id);
        }

        [Route("api/books/addAll")]
        [HttpPut]
        public void AddAllToShelf()
        {
            BookManager.Singleton.AddAllToShelf();
        }

        [Route("api/books/rejectAll")]
        [HttpPut]
        public void AddAllToRejected()
        {
            BookManager.Singleton.AddAllToShelf();
        }

        [Route("api/books/removeNeutral/{id}")]
        [HttpPut]
        public void RemoveNeutralBook(long id)
        {
            BookManager.Singleton.RemoveNeutralBook(id);
        }

        [Route("api/books/removeShelved/{id}")]
        [HttpPut]
        public void RemoveShelvedBook(long id)
        {
            BookManager.Singleton.RemoveShelvedBook(id);
        }

        [Route("api/books/removeRejected/{id}")]
        [HttpPut]
        public void RemoveRejectedBook(long id)
        {
            BookManager.Singleton.RemoveNeutralBook(id);
        }

        [Route("api/books/resetAll")]
        [HttpPut]
        public void ResetAllBooks()
        {
            BookManager.Singleton.ResetAllBooks();
        }

        [Route("api/books/finishedBook/{id}")]
        [HttpPut]
        public void FinishedBook(int id)
        {
            BookManager.Singleton.FinishedBook(id);
        }

        [Route("api/books/finishedToString/{name}/{id}")]
        [HttpGet]
        public string FinishedToString(string name, long id)
        {
            return BookManager.Singleton.FinishedToString(name, id);
        }
    }
}
