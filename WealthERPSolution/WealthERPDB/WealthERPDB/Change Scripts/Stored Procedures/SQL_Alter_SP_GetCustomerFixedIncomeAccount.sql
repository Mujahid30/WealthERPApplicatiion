-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerFixedIncomeAccount]

@CFIA_AccountId INT

AS

SELECT * FROM dbo.CustomerFixedIncomeAccount WHERE CFIA_AccountId=@CFIA_AccountId 