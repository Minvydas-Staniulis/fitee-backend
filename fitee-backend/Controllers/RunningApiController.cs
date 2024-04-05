using fitee_backend.Data;
using fitee_backend.DataAccess;
using fitee_backend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        [HttpGet]
        [Route("api/[controller]/GetRunnings")]
        public IActionResult Get(string name = null)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<RunningModel> data = _db.GetRunnings(name);
                
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

        [HttpGet]
        [Route("api/[controller]/TotalRunDistance")]
        public IActionResult GetTotalRunDistance()
        {
            try
            {
                List<RunningModel> runnings = _db.GetRunnings();

                float totalRunDistance = _db.GetTotalRunDistance(runnings);

                var result = new { totalDistance = totalRunDistance };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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

        [HttpPost]
        [Route("api/[controller]/AddRunning")]
        public IActionResult Post([FromBody] RunningModel runningModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                double paceMinutes = runningModel.running_time.TotalMinutes / runningModel.distance;
                runningModel.pace = TimeSpan.FromMinutes(paceMinutes);

                _db.SaveRunning(runningModel);
                return Ok(ResponseHandler.GetAppResponse(type, runningModel));
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON payload for running_time property.");
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateRunning/{id}")]
        public IActionResult Put(int id, [FromBody] RunningModel runningModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                runningModel.id = id;
                _db.SaveRunning(runningModel);
                return Ok(ResponseHandler.GetAppResponse(type, runningModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteRunning/{id}")]
        public IActionResult Delete(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                _db.DeleteRunning(id);
                return Ok(ResponseHandler.GetAppResponse(type, null));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
