using fitee_backend.Data;
using fitee_backend.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fitee_backend.Controllers
{
    
    [ApiController]
    public class RunningApiController : ControllerBase
    {
        private readonly DbHelper _db;

        public RunningApiController(AppDbContext appDbContext)
        {
            _db = new DbHelper(appDbContext);

        }
        // GET: api/<RunningApi>
        [HttpGet]
        [Route("api/[controller]/GetRunnings")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<RunningModel> data = _db.GetRunnings();
                
                if(!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex) {

                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<RunningApi>/5
        [HttpGet]
        [Route("api/[controller]/GetRunningById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
               RunningModel data = _db.GetRunningById(id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {

                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<RunningApi>
        [HttpPost]
        [Route("api/[controller]/AddRunning")]
        public IActionResult Post([FromBody] RunningModel runningModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                _db.SaveRunning(runningModel);
                return Ok(ResponseHandler.GetAppResponse(type, runningModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<RunningApi>/5
        [HttpPut]
        [Route("api/[controller]/UpdateRunning/{id}")]
        public IActionResult Put(int id, [FromBody] RunningModel runningModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                runningModel.id = id; // Ensure the ID matches the URL
                _db.SaveRunning(runningModel);
                return Ok(ResponseHandler.GetAppResponse(type, runningModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<RunningApi>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteRunning/{id}")]
        public IActionResult Delete(int id, RunningModel runningModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                _db.DeleteRunning(id);
                return Ok(ResponseHandler.GetAppResponse(type, runningModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
