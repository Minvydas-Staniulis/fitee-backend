using fitee_backend.Data;
using fitee_backend.Model;
using System.Xml.Linq;

namespace fitee_backend.DataAccess
{
    public class DbHelper
    {
        private AppDbContext _context;
        public DbHelper(AppDbContext context)
        {
            _context = context;
        }
        public List<RunningModel> GetRunnings(string name = null)
        {
            List<RunningModel> response = new List<RunningModel>();

            IQueryable<Running> query = _context.Runnings;

            if(!string.IsNullOrEmpty(name) )
            {
                query = query.Where(r => r.name.Contains(name));
            }

            var dataList = query.ToList();

            dataList.ForEach(row => response.Add(new RunningModel()
            {
                id = row.id,
                name = row.name,
                distance = row.distance,
                running_time = row.running_time,
                pace = row.pace,
            }));

            return response;
        }

        public RunningModel GetRunningById(int id)
        {
            var dataList = _context.Runnings.FirstOrDefault(d => d.id == id);
            if (dataList == null)
            {
                throw new Exception("Running record not found with ID: " + id);
            }

            return new RunningModel()
            {
                id = dataList.id,
                name = dataList.name,
                distance = dataList.distance,
                running_time = dataList.running_time,
                pace = dataList.pace,
            };
        }

        public float GetTotalRunDistance(List<RunningModel> response)
        {
            float totalDistance = 0;

            foreach (var row in response)
            {
                totalDistance += row.distance;
            }

            return totalDistance;
        }

        public void SaveRunning(RunningModel runningModel)
        {
            Running dbTable = null;
            if (runningModel.id > 0)
            {
                dbTable = _context.Runnings.FirstOrDefault(d => d.id == runningModel.id);
                if (dbTable == null)
                {
                    throw new Exception("Running record not found with ID: " + runningModel.id);
                }
            }
            else
            {
                dbTable = new Running();
                _context.Runnings.Add(dbTable);
            }

            dbTable.name = runningModel.name;
            dbTable.distance = runningModel.distance;
            dbTable.running_time = runningModel.running_time;
            dbTable.pace = runningModel.pace;

            System.Diagnostics.Debug.WriteLine(dbTable.pace);

            _context.SaveChanges();
        }

        public void DeleteRunning(int id)
        {
            var dbTable = _context.Runnings.FirstOrDefault(d => d.id == id);
            if (dbTable == null)
            {
                throw new Exception("Running record not found with ID: " + id);
            }

            _context.Runnings.Remove(dbTable);
            _context.SaveChanges();
        }
    }


}
