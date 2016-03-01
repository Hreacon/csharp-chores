using Xunit;
using ChoresNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ChoresNS
{
  public class ChoresTest : IDisposable
  {
     public ChoresTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=chores_test;Integrated Security=SSPI;";
     }
     [Fact]
     public void JobEqualsJob()
     {
       Job job1 = new Job("j", 1);
       Job job2 = new Job("j", 1);

       Assert.Equal(job1, job2);
     }
     [Fact]
     public void JobSavesAndGetsAll()
     {
       Job j = new Job("J");
       j.Save();
       Assert.Equal(1, Job.GetAll().Count);
     }
     [Fact]
     public void DeleteJob()
     {
       Job newJob = new Job("j");
       newJob.Save();
       List<Job> beforeList = Job.GetAll();
       newJob.Delete();
       List<Job> afterList = Job.GetAll();

       Assert.Equal(beforeList.Count, 1);
       Assert.Equal(afterList.Count, 0);
     }
     [Fact]
     public void JobFindById()
     {
       Job newJob = new Job("test");
       newJob.Save();
       Assert.Equal(newJob, Job.Find(newJob.GetId()));
     }
     [Fact]
     public void JobFindByName()
     {
       Job newJob = new Job("Dishes");
       newJob.Save();
       Assert.Equal(newJob, Job.Find(newJob.GetName()));
     }
     [Fact]
     public void WorkerEqualsWorker()
     {
       Worker worker1 = new Worker("j", 1);
       Worker worker2 = new Worker("j", 1);

       Assert.Equal(worker1, worker2);
     }
     [Fact]
     public void WorkerSavesAndGetsAll()
     {
       Worker j = new Worker("J");
       j.Save();
       Assert.Equal(1, Worker.GetAll().Count);
     }
     [Fact]
     public void DeleteWorker()
     {
       Worker newWorker = new Worker("j");
       newWorker.Save();
       List<Worker> beforeList = Worker.GetAll();
       newWorker.Delete();
       List<Worker> afterList = Worker.GetAll();

       Assert.Equal(beforeList.Count, 1);
       Assert.Equal(afterList.Count, 0);
     }
     [Fact]
     public void WorkerFindById()
     {
       Worker newWorker = new Worker("test");
       newWorker.Save();
       Assert.Equal(newWorker, Worker.Find(newWorker.GetId()));
     }
     [Fact]
     public void WorkerFindByName()
     {
       Worker newWorker = new Worker("Dishes");
       newWorker.Save();
       Assert.Equal(newWorker, Worker.Find(newWorker.GetName()));
     }
     [Fact]
     public void TestGetWorkers()
     {
       Worker w = new Worker("Wesley");
       Job j = new Job("Clean Dishes");
       w.Save();
       j.Save();
       w.AddAssignment(j.GetId());
       Job test = w.GetJobs()[0];
       Assert.Equal(j, test);
     }
     public void Dispose()
     {
       Job.DeleteAll();
       Worker.DeleteAll();
     }
  }
}
