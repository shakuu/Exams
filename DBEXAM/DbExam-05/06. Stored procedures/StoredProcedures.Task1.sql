/* Returning all computers (vendor, model, id) with memory (RAM) between provided range
	i.e. usp_GetComputersWithRamBetween(1 GB, 8 GB)*/

USE ComputersDb
GO
CREATE PROC dbo.usp_GetComputersWithRamBetween
	 @minimumRAM INT,
	 @maximumRAM INT
	AS 
		SELECT [Id], [Vendor], [Model]
		FROM Computers
		WHERE @minimumRAM <= Computers.Memory AND Computers.Memory <= @maximumRAM
GO	

EXEC dbo.usp_GetComputersWithRamBetween 6, 12