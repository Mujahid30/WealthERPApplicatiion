-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerInfo]
@U_UserId bigint
as
select * from Customer where U_UMId=@U_UserId
 