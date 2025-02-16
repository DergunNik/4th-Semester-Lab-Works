using BrigadeManager.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();
            var random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                    await unitOfWork.BrigadeRepository.AddAsync(new Brigade
                    {
                        Id = i,
                        Name = $"Brigade {i}",
                        Leader = $"Leader {i}",
                        WorkersNumber = random.Next(10, 20),
                        Works = new List<Work>()
                    });
            };

            await unitOfWork.SaveAllAsync();

            int id = 1;
            for (int i = 1; i <= 10; i++) 
                for (int j = 1; j <= random.Next(3, 10); ++j) 
                    await unitOfWork.WorkRepository.AddAsync(new Work
                    {
                        Name = $"Work {id}",
                        Id = id++,
                        Rating = random.Next(1, 11),
                        StartDate = new DateTime(2025, i, j + random.Next(1, 10)),
                        EndDate = new DateTime(2025, i + 1, j + random.Next(1, 10)),
                        BrigadeId = i,
                        ImageSrc = "default_image.jpg"
                    });
            
            await unitOfWork.SaveAllAsync();
        }
    }

}
