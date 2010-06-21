-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateUser]
@U_UserId INT,
@U_FirstName varchar(50),
@U_MiddleName varchar(50),
@U_LastName varchar(50),
@U_Email varchar(MAX),
@U_LoginId varchar(MAX),
@U_Password varchar(50)


as

update [User] set 
U_FirstName=@U_FirstName,
U_MiddleName=@U_MiddleName,
U_LastName=@U_LastName,
U_Email=@U_Email,
U_LoginId=@U_LoginId,
U_Password=@U_Password 
where 
U_UserId=@U_UserId
 