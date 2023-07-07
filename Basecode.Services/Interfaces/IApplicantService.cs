﻿using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantService
    {
        List<Applicant> GetApplicants();

        Applicant GetApplicantById(int id);
        LogContent Create(ApplicantViewModel applicant);
    }
}
