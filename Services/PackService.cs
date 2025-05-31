using CromulentBisgetti;
using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using Microsoft.EntityFrameworkCore;
using PackSolverAPI.DbContexts;
using PackSolverAPI.Models;

namespace PackSolverAPI.Services
{
    public class PackService
    {
        public List<Box> Pack(List<Item> items, List<Box> boxes)
        {
            List<Box> selectedBoxes = new List<Box>();
            var algorithm = new List<int> { (int)AlgorithmType.EB_AFIT };

            var containers = boxes
                .Select((b, index) => new Container(index, b.Height, b.Width, b.Length))
                .ToList();

            while (true)
            {
                var result = PackingService.Pack(containers, items, algorithm);

                var bestContainer = result
                    .Where(container => container.AlgorithmPackingResults.FirstOrDefault().PercentContainerVolumePacked > 0)
                    .OrderByDescending(container => container.AlgorithmPackingResults.FirstOrDefault().PercentContainerVolumePacked)
                    .FirstOrDefault();

                if (bestContainer == null)
                {
                    return selectedBoxes;
                }

                selectedBoxes.Add(boxes[bestContainer.ContainerID]);

                if (bestContainer.AlgorithmPackingResults.FirstOrDefault().IsCompletePack)
                {
                    return selectedBoxes;
                }

                var unpackedItems = bestContainer.AlgorithmPackingResults.FirstOrDefault().UnpackedItems;
                items = unpackedItems.Select(item => new Item(item.ID, item.Dim1, item.Dim2, item.Dim3, item.Quantity)).ToList();
            }
        }
    }
}
