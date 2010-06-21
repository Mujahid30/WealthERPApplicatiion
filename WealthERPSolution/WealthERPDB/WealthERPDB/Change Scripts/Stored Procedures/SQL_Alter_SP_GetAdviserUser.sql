-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetAdviserUser]
@U_UserId BIGINT
as
select * from Adviser where U_UserId=@U_UserId
 