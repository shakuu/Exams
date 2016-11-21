/*Returning all computers with a specific GPU (by id) and range of memory (as the above)
i.e. usp_GetComputersWithGpuAndRamBetween(2, 8 GB, 16 GB)*/

USE ComputersDb
GO
CREATE PROC dbo.usp_GetComputersWithGpuAndRamBetween
	 @GpuId INT,
	 @minimumRAM INT,
	 @maximumRAM INT
	AS 
		SELECT [Id], [Vendor], [Model]
		FROM Computers, Computers_GPUs
		WHERE @minimumRAM <= Computers.Memory AND Computers.Memory <= @maximumRAM AND 
			Computers.Id = Computers_GPUs.ComputerId AND Computers_GPUs.GPUId = @GpuId
GO	

exec dbo.usp_GetComputersWithGpuAndRamBetween 15, 0, 30