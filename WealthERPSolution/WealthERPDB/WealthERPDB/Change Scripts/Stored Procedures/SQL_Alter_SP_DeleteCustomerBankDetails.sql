ALTER PROCEDURE SP_DeleteCustomerBankDetails
@CB_CustBankAccId INT

AS

BEGIN
	
	DELETE FROM CustomerBank
	WHERE CB_CustBankAccId = @CB_CustBankAccId
	
END 