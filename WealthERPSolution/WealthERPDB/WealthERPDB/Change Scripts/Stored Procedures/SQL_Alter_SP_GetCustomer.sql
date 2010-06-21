-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomer]
@C_CustomerId bigint
as
select * from Customer where C_CustomerId=@C_CustomerId 