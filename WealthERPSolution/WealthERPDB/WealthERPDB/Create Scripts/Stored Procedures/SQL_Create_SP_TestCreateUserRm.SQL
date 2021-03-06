/****** Object:  StoredProcedure [dbo].[SP_TestCreateUserRm]    Script Date: 06/24/2009 12:05:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_TestCreateUserRm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_TestCreateUserRm]
GO
/****** Object:  StoredProcedure [dbo].[SP_TestCreateUserRm]    Script Date: 06/24/2009 12:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_TestCreateUserRm]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_TestCreateUserRm 

@AR_RMId	int,
@A_AdviserId	int,
@U_UserId	int,
@AR_FirstName	varchar(50),
@AR_MiddleName	varchar(50),
@AR_LastName	varchar(50),
@AR_OfficePhoneDirectISD	numeric(4, 0),
@AR_OfficePhoneDirectSTD	numeric(4, 0),
@AR_OfficePhoneDirect	numeric(8, 0),
@AR_OfficePhoneExtISD	numeric(4, 0),
@AR_OfficePhoneExtSTD	numeric(4, 0),
@AR_OfficePhoneExt	numeric(8, 0),
@AR_ResPhoneISD	numeric(4, 0),
@AR_ResPhoneSTD	numeric(4, 0),
@AR_ResPhone	numeric(8, 0),
@AR_Mobile	numeric(10, 0),
@AR_FaxISD	numeric(4, 0),
@AR_FaxSTD	numeric(4, 0),
@AR_Fax	numeric(8, 0),
@AR_Email	varchar(MAX),
@AR_CreatedBy	int,
@AR_CreatedOn	datetime,
@AR_ModifiedBy	int,
@AR_ModifiedOn	datetime,
@AR_JobFunction	varchar(30),
@U_Password	varchar(50),
@U_FirstName	varchar(50),
@U_MiddleName	varchar(50),
@U_LastName	varchar(50),
@U_Email	varchar(MAX),
@U_UserType	varchar(10),
@U_LoginId	varchar(MAX)


AS

BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
		Declare  
	  @bTran AS INT,      
	  @lErrCode AS INT

	-- Begin Tran
	If (@@Trancount = 0)  
	 Begin  
	  Set @bTran = 1  
	  Begin Transaction  
	 End   
	 
	 exec SP_CreateUser 
					@U_Password,
@U_FirstName,
@U_MiddleName,
@U_LastName,
@U_Email,
@U_UserType,
@U_LoginId,
@AR_CreatedBy,
@AR_ModifiedBy,
@AR_CreatedOn,
@AR_ModifiedOn,
@U_UserId out

EXEC SP_CreateAdviserStaff
@AR_RMId,
@A_AdviserId,
@U_UserId,
@AR_FirstName,
@AR_MiddleName,
@AR_LastName,
@AR_OfficePhoneDirectISD,
@AR_OfficePhoneDirectSTD,
@AR_OfficePhoneDirect,
@AR_OfficePhoneExtISD,
@AR_OfficePhoneExtSTD,
@AR_OfficePhoneExt,
@AR_ResPhoneISD,
@AR_ResPhoneSTD,
@AR_ResPhone,
@AR_Mobile,
@AR_FaxISD,
@AR_FaxSTD,
@AR_Fax,
@AR_Email,
@AR_CreatedBy,
@AR_CreatedOn,
@AR_ModifiedBy,
@AR_ModifiedOn,
@AR_JobFunction





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
' 
END
GO
