-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateMFAccountAssociation]
@CMFA_AccountId INT,
@CA_AssociationId INT,
@CMFAA_AssociationType VARCHAR(30),
@CMFAA_CreatedBy INT,
@CMFAA_ModifiedBy INT
AS
INSERT INTO CustomerMutualFundAccountAssociates 
(
CMFA_AccountId,
CA_AssociationId,
CMFAA_AssociationType,
CMFAA_CreatedBy,
CMFAA_CreatedOn,
CMFAA_ModifiedBy,
CMFAA_ModifiedOn
)
VALUES
(
@CMFA_AccountId,
@CA_AssociationId,
@CMFAA_AssociationType,
@CMFAA_CreatedBy,
CURRENT_TIMESTAMP,
@CMFAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 