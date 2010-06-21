-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetRMCustomerListDataSet]
@AR_RMId INT

AS

BEGIN
	
	SELECT 
		C_CustomerId AS CustomerID,
		C_FirstName+' '+C_LastName AS CustomerName,
		U_UMId AS UserID
	FROM
		dbo.Customer
	WHERE
		AR_RMId = @AR_RMId
	
END


 