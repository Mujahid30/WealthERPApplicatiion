-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateFixedIncomeAccountAssociation]
@CFIA_AccountId INT,
@CA_AssociateId INT,
@CFIAA_AssociationType VARCHAR(30),
@CFIAA_CreatedBy INT,
@CFIAA_ModifiedBy INT
AS
INSERT INTO CustomerFixedIncomeAcccountAssociates 
(
CFIA_AccountId,
CA_AssociateId,
CFIAA_AssociationType,
CFIAA_CreatedBy,
CFIAA_CreatedOn,
CFIAA_ModifiedBy,
CFIAA_ModifiedOn
)
VALUES
(
@CFIA_AccountId,
@CA_AssociateId,
@CFIAA_AssociationType,
@CFIAA_CreatedBy,
CURRENT_TIMESTAMP,
@CFIAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 