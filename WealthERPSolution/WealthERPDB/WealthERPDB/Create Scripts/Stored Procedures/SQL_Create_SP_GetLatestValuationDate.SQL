/****** Object:  StoredProcedure [dbo].[SP_GetLatestValuationDate]    Script Date: 06/24/2009 12:03:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetLatestValuationDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetLatestValuationDate]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLatestValuationDate]    Script Date: 06/24/2009 12:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetLatestValuationDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetLatestValuationDate

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

--EXEC [SP_GetLatestValuationDate] 1004,''EQ''
' 
END
GO
