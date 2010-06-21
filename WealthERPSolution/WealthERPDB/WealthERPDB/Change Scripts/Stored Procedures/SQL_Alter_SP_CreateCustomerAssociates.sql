
ALTER PROCEDURE [dbo].[SP_CreateCustomerAssociates]

@C_AssociateCustomerId int,
@XR_RelationshipCode varchar(5),
@C_CustomerId int,
@CA_CreatedBy	int	,
@CA_ModifiedBy	int	

as
insert into CustomerAssociates
(
C_AssociateCustomerId,
XR_RelationshipCode,
C_CustomerId,
CA_CreatedBy,
CA_CreatedOn,
CA_ModifiedBy,
CA_ModifiedOn) 

values(

@C_AssociateCustomerId,
@XR_RelationshipCode,
@C_CustomerId,
@CA_CreatedBy,
current_timestamp,
@CA_ModifiedBy,
current_timestamp
)
 