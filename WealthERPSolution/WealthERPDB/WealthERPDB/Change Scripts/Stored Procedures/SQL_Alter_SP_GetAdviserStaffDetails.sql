-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetAdviserStaffDetails]
@U_UserId int
as
select * from AdviserRM where U_UserId=@U_UserId 