-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetUser]
@U_LoginId varchar(50)
as
select * from [User] where U_LoginId=@U_LoginId
 