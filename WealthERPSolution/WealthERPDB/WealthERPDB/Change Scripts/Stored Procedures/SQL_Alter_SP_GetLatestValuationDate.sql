-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetLatestValuationDate

@A_AdviserId INT,
@AssetGroup VARCHAR(50)

AS

SET NOCOUNT OFF;

BEGIN
DECLARE @dt VARCHAR(10)


	SELECT 
		 TOP 1   CONVERT(VARCHAR(10), ADEL_ProcessDate,101) AS ProcessDate
		 --CAST(ADEL_ProcessDate AS DateTime)
		 --
	FROM 
		AdviserDailyEODLog 
	WHERE 
		A_AdviserId=@A_AdviserId AND [ADEL_AssetGroup]=@AssetGroup
	ORDER BY [ADEL_ProcessDate] DESC
	
END

SET NOCOUNT ON;

--EXEC [SP_GetLatestValuationDate] 1004,'EQ'
 