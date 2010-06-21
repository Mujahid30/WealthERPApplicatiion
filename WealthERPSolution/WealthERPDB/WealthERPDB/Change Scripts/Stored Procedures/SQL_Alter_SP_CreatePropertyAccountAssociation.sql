-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreatePropertyAccountAssociation]
@CPA_AccountId INT,
@CA_AssociationId INT,
@CPAA_AssociationType VARCHAR(30),
@CPAA_NomineeShare NUMERIC(3,0),
@CPAA_CreatedBy INT,
@CPAA_ModifiedBy INT
AS
INSERT INTO CustomerPropertyAccountAssociates 
(
CPA_AccountId,
CA_AssociationId,
CPAA_AssociationType,
CPAA_NomineeShare,
CPAA_CreatedBy,
CPAA_CreatedOn,
CPAA_ModifiedBy,
CPAA_ModifiedOn
)
VALUES
(
@CPA_AccountId,
@CA_AssociationId,
@CPAA_AssociationType,
@CPAA_NomineeShare,
@CPAA_CreatedBy,
CURRENT_TIMESTAMP,
@CPAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 