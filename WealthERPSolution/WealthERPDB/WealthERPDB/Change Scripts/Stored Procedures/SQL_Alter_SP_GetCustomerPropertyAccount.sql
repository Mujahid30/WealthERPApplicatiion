ALTER PROCEDURE [dbo].[SP_GetCustomerPropertyAccount]
@CPA_AccountId INT

AS

BEGIN
	
	SELECT * FROM
	dbo.CustomerPropertyAccount
	WHERE CPA_AccountId = @CPA_AccountId
	
END 