using CromulentBisgetti;
using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;

namespace PackSolverAPI.Services
{
    public class PackService
    {
        public List<string> Pack(List<Item> items)
        {
            List<string> selectedBoxes = null;
            var algorithm = new List<int> { (int)AlgorithmType.EB_AFIT };

            var containers = new List<Container> {
                new Container(1, 30, 40, 80),
                new Container(2, 80, 50, 40),
                new Container(3, 50, 80, 60)
            };

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

                selectedBoxes.Add(bestContainer.ContainerID.ToString());

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
