-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCustomerPortfolio]
@C_CustomerId INT,
@CP_IsMainPortfolio TINYINT,
@CP_IsPMS TINYINT,
@CP_PMSIdentifier VARCHAR(20),
@CP_CreatedBy INT,
@CP_ModifiedBy INT,
@CP_PortfolioId INT OUTPUT

AS

SET NOCOUNT ON   

Declare  
  @bTran AS INT,      
  @lErrCode AS INT

-- Begin Tran
If (@@Trancount = 0)  
 Begin  
  Set @bTran = 1  
  Begin Transaction  
 End 

INSERT INTO dbo.CustomerPortfolio (
	C_CustomerId,
	CP_IsMainPortfolio,
	CP_IsPMS,
	CP_PMSIdentifier,
	CP_CreatedBy,
	CP_CreatedOn,
	CP_ModifiedBy,
	CP_ModifiedOn
) VALUES ( 
	@C_CustomerId,
	@CP_IsMainPortfolio,
	@CP_IsPMS,
	@CP_PMSIdentifier,
	@CP_CreatedBy,
	CURRENT_TIMESTAMP,
	@CP_ModifiedBy,
	CURRENT_TIMESTAMP ) 
	
	SELECT @CP_PortfolioId=SCOPE_IDENTITY()

Success:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin                                    
  Commit Tran      
 End      
 Return 0      
      
 Goto Done      
      
Error:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin      
  Rollback Transaction      
 End      
 Return @lErrCode      

	       
Done:            
SET NOCOUNT OFF       


 