-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreatePensionGratuitiesAccountAssociation]
@CPGA_AccountId INT,
@CA_AssociationId INT,
@CPGAA_AssociationType VARCHAR(30),
@CPGAA_NomineeShare NUMERIC(3,0),
@CPGAA_CreatedBy INT,
@CPGAA_ModifiedBy INT
AS
INSERT INTO CustomerPensionandGrauitiesAccountAssociates 
(
CPGA_AccountId,
CA_AssociationId,
CPGAA_AssociationType,
CPGAA_NomineeShare,
CPGAA_CreatedBy,
CPGAA_CreatedOn,
CPGAA_ModifiedBy,
CPGAA_ModifiedOn
)
VALUES
(
@CPGA_AccountId,
@CA_AssociationId,
@CPGAA_AssociationType,
@CPGAA_NomineeShare,
@CPGAA_CreatedBy,
CURRENT_TIMESTAMP,
@CPGAA_ModifiedBy,
CURRENT_TIMESTAMP
)
 