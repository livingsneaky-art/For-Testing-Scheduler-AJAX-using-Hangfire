﻿using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IJobOpeningService
    {
        List<JobOpeningViewModel> GetJobs();
        void Create(JobOpening jobOpening, string createdBy);

        JobOpening GetById(int id);

        void Update(JobOpening jobOpening, string updatedBy);

        void Delete(JobOpening jobOpening);
    }
}
