using Azure;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class PackageDescriptionService : IPackageDescriptionService
    {
        private readonly IPackageDescriptionRepo _repo;
        public PackageDescriptionService(IPackageDescriptionRepo packageDescriptionRepo)
        {
            _repo = packageDescriptionRepo;
        }
        public async Task<ServiceResponse<string>> AddPackageDescription(string name)
        {
            _ = _repo.Add(name);

            var dbResponse = await _repo.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            return ServiceResponse<string>.Success($"Package description was saved successfully.");
        }

        public ServiceResponse<List<PackageDescQueryRes>> GetPackageDescription()
        {
            var _descriptions = _repo.GetAll();

            var descriptions = _descriptions.Select(e => new PackageDescQueryRes { 
                Id = e.Id,
                Name = e.Name
            }).ToList();

            return ServiceResponse<List<PackageDescQueryRes>>.Success(descriptions);
        }

        public Task<ServiceResponse<string>> RemovePackageDescription(Guid id)
        {
            throw new ArgumentNullException(nameof(id));
        }
    }
}
