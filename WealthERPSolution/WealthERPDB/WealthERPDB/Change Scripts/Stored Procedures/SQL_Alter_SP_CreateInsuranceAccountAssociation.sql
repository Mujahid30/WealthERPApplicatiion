-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateInsuranceAccountAssociation]
@CIA_AccountId INT,
@CA_AssociationId INT,
@CIAA_AssociationType VARCHAR(30),
@CIAA_CreatedBy INT,
@CIAA_ModifiedBy INT
AS
INSERT INTO CustomerInsuranceAccountAssociates 
(
CIA_AccountId,
CA_AssociationId,
CIAA_AssociationType,
CIAA_CreatedBy,
CIAA_CreatedOn,
CIAA_ModifiedBy,
CIAA_ModifiedOn
)
VALUES
(
@CIA_AccountId,
@CA_AssociationId,
@CIAA_AssociationType,
@CIAA_CreatedBy,
CURRENT_TIMESTAMP,
@CIAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 