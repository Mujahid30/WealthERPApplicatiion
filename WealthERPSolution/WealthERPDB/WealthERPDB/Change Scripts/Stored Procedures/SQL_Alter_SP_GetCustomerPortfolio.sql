-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetCustomerPortfolio
@C_CustomerId INT

AS

SELECT * FROM dbo.CustomerPortfolio
WHERE
C_CustomerId=@C_CustomerId
 