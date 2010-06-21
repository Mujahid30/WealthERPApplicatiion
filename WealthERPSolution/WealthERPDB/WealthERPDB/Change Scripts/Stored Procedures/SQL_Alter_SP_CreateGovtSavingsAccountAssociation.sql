-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateGovtSavingsAccountAssociation]
@CGSA_AccountId INT,
@CA_AssociationId INT,
@CGSAA_AssociationType VARCHAR(30),
@CGSAA_CreatedBy INT,
@CGSAA_ModifiedBy INT
AS
INSERT INTO CustomerGovtSavingAccountAssociates 
(

CGSA_AccountId,
CA_AssociationId,
CGSAA_AssociationType,
CGSAA_CreatedBy,
CGSAA_CreatedOn,
CGSAA_ModifiedBy,
CGSAA_ModifiedOn
)
VALUES
(

@CGSA_AccountId,
@CA_AssociationId,
@CGSAA_AssociationType,
@CGSAA_CreatedBy,
CURRENT_TIMESTAMP,
@CGSAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 