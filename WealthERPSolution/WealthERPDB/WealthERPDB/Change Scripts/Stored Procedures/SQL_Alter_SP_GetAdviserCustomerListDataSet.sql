-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAdviserCustomerListDataSet]
@A_AdviserId INT

AS

BEGIN
	
	SELECT 
		C_CustomerId AS CustomerID,
		C_FirstName+' '+C_LastName AS CustomerName,
		U_UMId AS UserID 
	FROM  Customer 
	INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
	INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId
	WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
	
	
END


 