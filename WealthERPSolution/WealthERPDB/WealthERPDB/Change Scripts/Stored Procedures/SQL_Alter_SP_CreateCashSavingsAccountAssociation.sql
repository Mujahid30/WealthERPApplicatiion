-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCashSavingsAccountAssociation]
@CCSA_AccountId INT,
@CA_AssociationId INT,
@CCSAA_AssociationType VARCHAR(30),
@CCSAA_CreatedBy INT,
@CCSAA_ModifiedBy INT
AS
INSERT INTO CustomerCashSavingsAccountAssociates 
(
CCSA_AccountId,
CA_AssociationId,
CCSAA_AssociationType,
CCSAA_CreatedBy,
CCSAA_CreatedOn,
CCSAA_ModifiedBy,
CCSAA_ModifiedOn
)
VALUES
(
@CCSA_AccountId,
@CA_AssociationId,
@CCSAA_AssociationType,
@CCSAA_CreatedBy,
CURRENT_TIMESTAMP,
@CCSAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 