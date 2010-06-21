-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateAdviserStaff]
@A_AdviserId int,
@U_UserId int,
@AR_FirstName varchar(50),
@AR_MiddleName varchar(50),
@AR_LastName varchar(50),
@AR_OfficePhoneDirectISD numeric(4,0),
@AR_OfficePhoneDirectSTD numeric(4,0),
@AR_OfficePhoneDirect numeric(8,0),
@AR_OfficePhoneExtISD numeric(4,0),
@AR_OfficePhoneExtSTD numeric(4,0),
@AR_OfficePhoneExt numeric(8,0),
@AR_ResPhoneISD numeric(4,0),
@AR_ResPhoneSTD numeric(4,0),
@AR_ResPhone numeric(8,0),
@AR_Mobile numeric(10,0),
@AR_FaxISD numeric(4,0),
@AR_FaxSTD numeric(4,0),
@AR_Fax numeric(8,0),
@AR_Email varchar(50),
@AR_JobFunction VARCHAR(30),
@AR_CreatedBy	varchar(50),
@AR_ModifiedBy	varchar(50),
@AR_RMId INT OUTPUT 




as

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

insert into dbo.AdviserRM (
	A_AdviserId,
	U_UserId,
	AR_FirstName,
	AR_MiddleName,
	AR_LastName,
	AR_OfficePhoneDirectISD,
	AR_OfficePhoneDirectSTD,
	AR_OfficePhoneDirect,
	AR_OfficePhoneExtISD,
	AR_OfficePhoneExtSTD,
	AR_OfficePhoneExt,
	AR_ResPhoneISD,
	AR_ResPhoneSTD,
	AR_ResPhone,
	AR_Mobile,
	AR_FaxISD,
	AR_FaxSTD,
	AR_Fax,
	AR_Email,
	AR_JobFunction,
	AR_CreatedBy,
	AR_CreatedOn,
	AR_ModifiedBy,
	AR_ModifiedOn
	
) 
values
(@A_AdviserId,
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
@AR_JobFunction,
@AR_CreatedBy,
current_timestamp,
@AR_ModifiedBy,
current_timestamp
)
SELECT @AR_RMId=SCOPE_IDENTITY()
--go

--exec createadvisorstaff
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

 