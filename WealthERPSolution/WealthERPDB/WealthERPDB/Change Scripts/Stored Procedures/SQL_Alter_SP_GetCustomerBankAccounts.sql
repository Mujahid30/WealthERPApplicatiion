-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerBankAccounts] 
@C_CustomerId int
AS

SELECT * FROM dbo.CustomerBank WHERE C_CustomerId=@C_CustomerId

 