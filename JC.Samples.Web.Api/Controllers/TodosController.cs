using JC.Samples.Web.Api.Errors;
using JC.Samples.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace JC.Samples.Web.Api.Controllers
{
    /// <summary>
    /// Exposes Todo endpoints.
    /// </summary>
    public class TodosController : ApiController
    {
        #region Fields

        /// <summary>
        /// Holds an in-memory list of Todo items for simulation purposes.
        /// </summary>
        private static IEnumerable<Todo> _todos = new List<Todo>
            {
                new Todo { Id = 1, Title = "Buy milk", UserId = 1 },
                new Todo { Id = 2, Title = "Leave out the trash", UserId = 2 },
                new Todo { Id = 3, Title = "Clean room", UserId = 2 }
            };

        #endregion

        #region Methods

        /// <summary>
        /// Gets all Todos.
        /// </summary>
        /// <param name="userId">The ID of the User to get Todos for (optional)</param>
        /// <returns>A collection of all available Todos if any were found, otherwise null</returns>
        public async Task<IHttpActionResult> Get(int? userId = null)
        {
            // Simulate an async database call.
            var todos = await Task.Run(() => 
                userId != null && userId > 0 ? 
                    _todos.Where(t => t.UserId == userId) : 
                    _todos);

            // Return the Todos, if any are found.
            if (todos != null && todos.Any()) return Ok(todos);

            // Fire an error and return a 'Not Found' status if there are no Todos.
            throw new ApiException(HttpStatusCode.NotFound, ApiErrorCode.ResourceNotFound);
        }

        #endregion
    }
}