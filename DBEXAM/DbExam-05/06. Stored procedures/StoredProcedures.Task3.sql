/*Return all GPUs that can be paired with a concrete computer type (dekstop, ultrabook or notebook)
	i.e. usp_GetGpusForComputerType("ultrabook")*/

USE ComputersDb
GO
CREATE PROC dbo.usp_GetGpusForComputerType
	@ComputerType NVARCHAR(50)
AS
	BEGIN
		DECLARE @type INT
		IF @ComputerType = 'dekstop'
			SET @type = 0

		IF @ComputerType = 'ultrabook'
			SET @type = 1

		IF @ComputerType = 'notebook'
			SET @type = 2

		SELECT *
		FROM GPUs
		WHERE GPUs.Type = @type
	END

GO

exec usp_GetGpusForComputerType 'ultrabook'