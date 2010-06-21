-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetUserDetails]
@U_UserId int
as
select * from [User] where U_UserId=@U_UserId
 