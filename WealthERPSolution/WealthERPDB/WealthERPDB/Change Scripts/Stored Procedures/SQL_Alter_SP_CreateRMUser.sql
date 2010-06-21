-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateRMUser]
@U_UserId int,
@U_Password varchar(50),
@U_FirstName varchar(50),
@U_MiddleName varchar(50),
@U_Lastname varchar(50),
@U_Email varchar(MAX),
@U_UserType varchar(10)

as

insert into dbo.[User]
(
U_UserId,
U_Password,
U_FirstName,
U_MiddleName,
U_Lastname,
U_Email,
U_UserType)
 values(
 @U_UserId,
 @U_Password,
 @U_FirstName,
 @U_MiddleName,
 @U_LastName,
 @U_Email,
 @U_UserType)
 