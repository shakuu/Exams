using System.Collections.Generic;
using System.Linq;

using DbExam.DatabaseFirst.ConsoleApp.Utils;
using DbExam.DatabaseFirst.Data;

namespace DbExam.DatabaseFirst.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            var randomDataGenerator = new RandomDataGenerator();

            Program.GenerateGPUs(randomDataGenerator, 30);
            Program.GenerateCPUs(randomDataGenerator, 30);
            Program.GenerateStorageDevices(randomDataGenerator, 30);

            Program.GenerateComputers(randomDataGenerator, 50);
        }

        private static ComputersDbEntities GetContext()
        {
            var context = new ComputersDbEntities();
            return context;
        }

        private static void GenerateComputers(RandomDataGenerator dataGenerator, int computerCount)
        {
            var context = Program.GetContext();

            var gpuIds = context.GPUs.Select(g => g.Id).ToList();
            var cpuIds = context.CPUs.Select(c => c.Id).ToList();
            var storageIds = context.StorageDevices.Select(s => s.Id).ToList();

            for (int i = 0; i < computerCount; i++)
            {
                var nextComputerInstance = new Computer();

                nextComputerInstance.Model = dataGenerator.GenerateString(20);
                nextComputerInstance.Vendor = dataGenerator.GenerateString(20);
                nextComputerInstance.Memory = dataGenerator.GenerateIntValue(64, 4);
                nextComputerInstance.ComputerType = dataGenerator.GenerateIntValue(3, 0);

                var nextCpuId = dataGenerator.GenerateIntValue(cpuIds.Count);
                nextComputerInstance.CpuId = cpuIds[nextCpuId];

                var gpusCount = dataGenerator.GenerateIntValue(4, 0);
                var gpus = new List<GPU>();
                for (int j = 0; j < gpusCount; j++)
                {
                    var nextGpuId = dataGenerator.GenerateIntValue(gpuIds.Count);
                    var nextGpu = context.GPUs.Find(gpuIds[nextGpuId]);

                    gpus.Add(nextGpu);
                }

                nextComputerInstance.GPUs = gpus;

                var storageCount = dataGenerator.GenerateIntValue(8, 0);
                var storages = new List<StorageDevice>();
                for (int k = 0; k < storageCount; k++)
                {
                    var nextStorageId = dataGenerator.GenerateIntValue(storageIds.Count);
                    var nextStorage = context.StorageDevices.Find(storageIds[nextStorageId]);

                    storages.Add(nextStorage);
                }

                nextComputerInstance.StorageDevices = storages;

                context.Computers.Add(nextComputerInstance);
            }

            context.SaveChanges();
        }

        private static void GenerateGPUs(RandomDataGenerator dataGenerator, int gpuCount)
        {
            var context = Program.GetContext();
            for (int i = 0; i < gpuCount; i++)
            {
                var nextGpuInstance = new GPU();

                nextGpuInstance.Memory = dataGenerator.GenerateIntValue(8, 1);
                nextGpuInstance.Model = dataGenerator.GenerateString(20);
                nextGpuInstance.Vendor = dataGenerator.GenerateString(20);
                nextGpuInstance.Type = dataGenerator.GenerateIntValue(2, 0);

                context.GPUs.Add(nextGpuInstance);
            }

            context.SaveChanges();
        }

        private static void GenerateCPUs(RandomDataGenerator dataGenerator, int cpuCount)
        {
            var context = Program.GetContext();
            for (int i = 0; i < cpuCount; i++)
            {
                var nextCpuInstance = new CPU();

                nextCpuInstance.Model = dataGenerator.GenerateString(20);
                nextCpuInstance.Vendor = dataGenerator.GenerateString(20);
                nextCpuInstance.Type = dataGenerator.GenerateIntValue(2, 0);
                nextCpuInstance.ClockCycles = dataGenerator.GenerateIntValue(4333, 33);

                context.CPUs.Add(nextCpuInstance);
            }

            context.SaveChanges();
        }

        private static void GenerateStorageDevices(RandomDataGenerator dataGenerator, int storageDevicesCount)
        {
            var context = Program.GetContext();
            for (int i = 0; i < storageDevicesCount; i++)
            {
                var nextStorageDeviceInstance = new StorageDevice();

                nextStorageDeviceInstance.Model = dataGenerator.GenerateString(20);
                nextStorageDeviceInstance.Vendor = dataGenerator.GenerateString(20);
                nextStorageDeviceInstance.Type = dataGenerator.GenerateIntValue(2, 0);
                nextStorageDeviceInstance.Size = dataGenerator.GenerateIntValue(10240, 128);

                context.StorageDevices.Add(nextStorageDeviceInstance);
            }

            context.SaveChanges();
        }
    }
}
