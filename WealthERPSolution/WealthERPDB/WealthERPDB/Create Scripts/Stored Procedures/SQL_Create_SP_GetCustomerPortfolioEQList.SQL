/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioEQList]    Script Date: 06/24/2009 12:00:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioEQList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPortfolioEQList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolioEQList]    Script Date: 06/24/2009 12:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolioEQList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE  procedure SP_GetCustomerPortfolioEQList
 
@CP_PortfolioId INT ,  
@A_AdviserId INT
as  

SET NOCOUNT ON

	SELECT 
		MAX(PEM_ScripCode) AS PEM_ScripCode,
		--CENP_CurrentValue,
		C_CustomerId,
		MAX(CETA_AccountId) AS CETA_AccountId,
		CENP_ValuationDate,
		PEM_CompanyName,
		PEM_Ticker  
	FROM 
		 ViewEquityNP
 
	WHERE
	( 
		 
		CP_PortfolioId=@CP_PortfolioId  
		AND
		--(A.CET_TradeDate <= @CET_TradeDate) 
		(CENP_ValuationDate < = (select top 1(ADEL_ProcessDate)
							FROM  							
								AdviserDailyEODLog
							WHERE 				
								A_AdviserId=@A_AdviserId
							order by ADEL_ProcessDate DESC))
	)
	
GROUP BY 

PEM_ScripCode,		
		CENP_CurrentValue,
		CETA_AccountId,
		C_CustomerId,
		CENP_ValuationDate,
		PEM_CompanyName,PEM_Ticker  
	
	
	ORDER BY CENP_CurrentValue
  
  
  

 
   
   
   
  
SET NOCOUNT OFF


--EXEC dbo.test 1632,1004
	

--EXEC dbo.SP_GetCustomerPortfolioEquityTransactionScripCodes 1636,1632,1004
--	@C_CustomerId = 0, -- INT
--	@CP_PortfolioId = 0, -- INT
--	@A_AdviserId = 0 -- INT

' 
END
GO
