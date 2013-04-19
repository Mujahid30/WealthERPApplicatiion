-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetUser]  
@U_LoginId varchar(50)  
as  
  
SELECT [U_UserId]  
      ,[U_Password]  
      ,[U_FirstName]  
      ,[U_MiddleName]  
      ,[U_LastName]  
      ,[U_Email]  
      ,[U_UserType]  
      ,[U_LoginId]  
      ,[U_CreatedBy]  
      ,[U_ModifiedBy]  
      ,[U_CreatedOn]  
      ,[U_ModifiedOn]  
      ,[U_IsTempPassword]  
      ,[U_PwdSaltValue]  
      ,[U_Theme]  
      ,dbo.Fn_GetAdvisorRMRoleList(U.U_UserId) RoleList  
      ,(SELECT COUNT(MR_MessageRecipientId) FROM MessageRecipients WHERE MR_RecipientId = [U_UserId] AND MR_ReadByUser = 0) AS UnreadMessages
      ,dbo.Fn_GetStaffPermissionList(U_UserId) AS PermissionList    
  FROM [User] U where U_LoginId=@U_LoginId  
  
SELECT * FROM [USER] where 1=2