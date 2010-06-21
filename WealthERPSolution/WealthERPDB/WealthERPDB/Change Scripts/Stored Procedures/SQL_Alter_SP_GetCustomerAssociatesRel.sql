-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerAssociatesRel]
@C_CustomerId INT
AS

BEGIN
	
	SELECT 
		dbo.CustomerAssociates.C_AssociateCustomerId, 
		dbo.CustomerAssociates.CA_AssociationId, 
		dbo.CustomerAssociates.XR_RelationshipCode, 
		dbo.Customer.C_FirstName, 
		dbo.Customer.C_LastName,
		XMLRelationship.XR_Relationship
	FROM 
		dbo.CustomerAssociates
		INNER JOIN
		dbo.Customer
		ON dbo.CustomerAssociates.C_AssociateCustomerId = dbo.Customer.C_CustomerId
		INNER JOIN
		dbo.XMLRelationship
		ON dbo.XMLRelationship.XR_RelationshipCode= dbo.CustomerAssociates.XR_RelationshipCode
	WHERE 
		dbo.CustomerAssociates.C_CustomerId=@C_CustomerId	
	
END
 