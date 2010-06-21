-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE  SP_GetCustomerEquityTransaction

@CET_EqTransId INT

AS

SELECT * FROM dbo.CustomerEquityTransaction WHERE  CET_EqTransId=@CET_EqTransId

 