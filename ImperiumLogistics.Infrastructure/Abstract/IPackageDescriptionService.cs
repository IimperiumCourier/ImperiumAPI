using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface IPackageDescriptionService
    {
        ServiceResponse<List<PackageDescQueryRes>> GetPackageDescription();
        Task<ServiceResponse<string>> AddPackageDescription(string name);
        Task<ServiceResponse<string>> RemovePackageDescription(Guid id);
    }
}
