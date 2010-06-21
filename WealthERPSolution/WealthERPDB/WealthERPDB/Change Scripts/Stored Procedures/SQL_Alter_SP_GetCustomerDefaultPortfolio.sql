
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerDefaultPortfolio]
@C_CustomerId INT

AS

SELECT * FROM dbo.CustomerPortfolio
WHERE
C_CustomerId=@C_CustomerId and CP_IsMainPortfolio=1

 