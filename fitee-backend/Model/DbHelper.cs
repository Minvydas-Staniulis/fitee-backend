﻿using fitee_backend.Data;

namespace fitee_backend.Model
{
    public class DbHelper
    {
        private AppDbContext _context;
        public DbHelper(AppDbContext context)
        {
            _context = context;
        }
        public List<RunningModel> GetRunnings()
        {
            List<RunningModel> response = new List<RunningModel>();
            var dataList = _context.Runnings.ToList();
            dataList.ForEach(row => response.Add(new RunningModel()
            {
                id = row.id,
                name =row.name,
                distance=row.distance,
                running_time=row.running_time,
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
            };
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
                dbTable = new Running(); // No need to set ID for new records
                _context.Runnings.Add(dbTable);
            }

            dbTable.name = runningModel.name;
            dbTable.distance = runningModel.distance;
            dbTable.running_time = runningModel.running_time;

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
            _context.SaveChanges(); // Save changes to the database
        }
    }

    
}
