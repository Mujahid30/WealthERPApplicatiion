-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerAssociates]
@C_CustomerId int
as
select * from CustomerAssociates where C_CustomerId=@C_CustomerId 