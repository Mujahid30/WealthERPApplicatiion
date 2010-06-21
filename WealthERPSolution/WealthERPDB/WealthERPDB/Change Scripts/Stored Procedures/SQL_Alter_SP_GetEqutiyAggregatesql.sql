	-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE SP_GetEqutiyAggregate  
  
@A_AdviserId INT,  
@AB_BranchId INT  
  
  
AS  
  
SELECT ISNULL( SUM(CENP_NetHoldings * CENP_MarketPrice),0) 
FROM dbo.ViewEquityNP AS VENP
Inner Join dbo.AdviserBranch AS AB ON VENP.A_AdviserId=AB.A_AdviserId
WHERE AB_BranchId=@AB_BranchId   