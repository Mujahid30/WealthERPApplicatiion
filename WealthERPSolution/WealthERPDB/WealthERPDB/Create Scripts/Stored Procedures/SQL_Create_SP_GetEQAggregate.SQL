/****** Object:  StoredProcedure [dbo].[SP_GetEQAggregate]    Script Date: 06/24/2009 12:02:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEQAggregate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetEQAggregate]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEQAggregate]    Script Date: 06/24/2009 12:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetEQAggregate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SP_GetEQAggregate]
@PortfolioId INT,  
@A_AdviserId INT 
  
AS  
SET NOCOUNT ON  
   
 SELECT  
  TOP 5
  PEM_CompanyName AS Script,  
  ISNULL(CAST((CENP_NetHoldings) AS INT), 0) AS NetHoldings,  
  ISNULL(CAST((CENP_NetHoldings * CENP_AveragePrice) AS INT), 0) AS AmortisedCost,  
  ISNULL(CAST((CENP_CurrentValue) AS INT), 0) AS CurrentValue  
 FROM  
 ViewEquityNP
 WHERE  
 CP_PortfolioId = @PortfolioId
 AND 
	CENP_ValuationDate in (
							select top 1(ADEL_ProcessDate)
							FROM  							
								AdviserDailyEODLog
							WHERE 				
								A_AdviserId=@A_AdviserId
							order by ADEL_ProcessDate desc
							
							)  
   ORDER BY CurrentValue desc
SET NOCOUNT OFF 


--EXEC testing 1632,1004



' 
END
GO
