/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerPortfolio]    Script Date: 06/24/2009 11:13:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerPortfolio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerPortfolio]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerPortfolio]    Script Date: 06/24/2009 11:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerPortfolio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCustomerPortfolio]
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


' 
END
GO
