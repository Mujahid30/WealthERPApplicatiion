ALTER PROCEDURE SP_DeleteCustomerAssociates
@CA_AssociationId INT

AS

BEGIN
	
	DELETE FROM dbo.CustomerAssociates
	WHERE CA_AssociationId = @CA_AssociationId
	
END 