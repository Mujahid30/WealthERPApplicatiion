  
  
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER procedure [dbo].[SP_GetCustomerPortfolioEquityTransactionScripCodes]  
@C_CustomerId INT ,  
@CP_PortfolioId INT ,  
@A_AdviserId INT,
@CET_TradeDate datetime  
as  

SET NOCOUNT ON

	SELECT TOP(5)
	D.C_CustomerId,	
		A.PEM_ScripCode,
		C.PEM_CompanyName,
			
		A.CETA_AccountId,				
		C.PEM_Ticker  
	FROM 
		CustomerEquityTransaction A
		--dbo.CustomerEquityNetPosition A
		INNER JOIN CustomerEquityTradeAccount B ON A.CETA_AccountId=B.CETA_AccountId 
		INNER JOIN ProductEquityMaster C ON A.PEM_ScripCode=C.PEM_ScripCode 
		INNER JOIN CustomerPortfolio D  ON B.CP_PortfolioId=D.CP_PortfolioId
	WHERE
	( 
		D.C_CustomerId=@C_CustomerId 
		AND 
		B.CP_PortfolioId=@CP_PortfolioId  
		AND
		--(A.CET_TradeDate <= @CET_TradeDate) 
		A.CET_TradeDate <= (select top 1(ADEL_ProcessDate)
							FROM  
							--dbo.ViewEquityNP  
							AdviserDailyEODLog
							WHERE  
								--CP_PortfolioId = @PortfolioId  
								--CP_PortfolioId =1632
								A_AdviserId=@A_AdviserId AND ADEL_AssetGroup='EQ'
							order by ADEL_ProcessDate desc
							)
	)
GROUP BY 
	D.C_CustomerId,
	A.PEM_ScripCode
	,A.CETA_AccountId,
	C.PEM_CompanyName,
	C.PEM_Ticker 
	
	
	ORDER BY A.PEM_ScripCode
  
SET NOCOUNT OFF


--EXEC dbo.SP_GetCustomerPortfolioEquityTransactionScripCodes 1636,1632,1004,"6/9/2009"
--	@C_CustomerId = 0, -- INT
--	@CP_PortfolioId = 0, -- INT
--	@A_AdviserId = 0 -- INT

 