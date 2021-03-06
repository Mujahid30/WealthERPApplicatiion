/****** Object:  StoredProcedure [dbo].[SP_CreateUser]    Script Date: 06/24/2009 11:14:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateUser]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateUser]    Script Date: 06/24/2009 11:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateUser]

@U_Password varchar(50),
@U_FirstName varchar(50),
@U_MiddleName varchar(50) = NULL,
@U_Lastname varchar(50) = NULL,
@U_Email varchar(MAX),
@U_UserType varchar(10),
@U_LoginId varchar(MAX),
@U_CreatedBy BIGINT,
@U_ModifiedBy BIGINT,
@U_UserId BIGINT OUTPUT

as

BEGIN
	
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
 
	IF (@U_Lastname <> NULL)
	BEGIN
		INSERT INTO [USER]
		(
			U_Password,
			U_FirstName,
			U_MiddleName,
			U_Lastname,
			U_Email,
			U_UserType,
			U_LoginId,
			U_CreatedBy,
			U_CreatedOn,
			U_ModifiedBy,
			U_ModifiedOn
		) 
		VALUES
		(
			@U_Password,
			@U_FirstName,
			@U_MiddleName,
			@U_LastName,
			@U_Email,
			@U_UserType,
			@U_LoginId,
			@U_CreatedBy,
			CURRENT_TIMESTAMP,
			@U_ModifiedBy,
			CURRENT_TIMESTAMP
		)

		SELECT @U_UserId = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		
		INSERT INTO [USER]
		(
			U_Password,
			U_FirstName,
			U_Email,
			U_UserType,
			U_LoginId,
			U_CreatedBy,
			U_CreatedOn,
			U_ModifiedBy,
			U_ModifiedOn
		) 
		VALUES
		(
			@U_Password,
			@U_FirstName,
			@U_Email,
			@U_UserType,
			@U_LoginId,
			@U_CreatedBy,
			CURRENT_TIMESTAMP,
			@U_ModifiedBy,
			CURRENT_TIMESTAMP
		)

		SELECT @U_UserId = SCOPE_IDENTITY()
		
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
	END
END

' 
END
GO
