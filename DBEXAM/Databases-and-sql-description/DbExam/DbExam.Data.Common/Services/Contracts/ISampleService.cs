using System;

using DbExam.Models;

namespace ExamPrep.Data.Common.Services.Contracts
{
    public interface ISampleService : IService<Superhero>
    {
        Superhero CreateSampleModel(string name, DateTime dateJoined, int age);
    }
}
